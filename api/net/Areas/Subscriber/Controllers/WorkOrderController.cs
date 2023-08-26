using System.Net;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Annotations;
using TNO.API.Config;
using TNO.API.Models;
using TNO.API.Models.SignalR;
using TNO.Core.Exceptions;
using TNO.Core.Extensions;
using TNO.DAL.Models;
using TNO.DAL.Services;
using TNO.Entities;
using TNO.Entities.Models;
using TNO.Kafka;
using TNO.Kafka.SignalR;
using TNO.Keycloak;

namespace TNO.API.Areas.Subscriber.Controllers;

/// <summary>
/// WorkOrderController class, provides WorkOrder endpoints for the api.
/// </summary>
[ClientRoleAuthorize(ClientRole.Subscriber)]
[ApiController]
[Area("subscriber")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[area]/work/orders")]
[Route("api/[area]/work/orders")]
[Route("v{version:apiVersion}/[area]/work/orders")]
[Route("[area]/work/orders")]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Unauthorized)]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Forbidden)]
public class WorkOrderController : ControllerBase
{
    #region Variables
    private readonly IWorkOrderService _workOrderService;
    private readonly IContentService _contentService;
    private readonly IUserService _userService;
    private readonly IKafkaMessenger _kafkaMessenger;
    private readonly KafkaOptions _kafkaOptions;
    private readonly KafkaHubConfig _kafkaHubOptions;
    private readonly JsonSerializerOptions _serializerOptions;

    // The following work order status ensure only a single request can be completed for content.
    private readonly IEnumerable<WorkOrderStatus> _workLimiterStatus = new[] { WorkOrderStatus.Submitted, WorkOrderStatus.InProgress };
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of a WorkOrderController object, initializes with specified parameters.
    /// </summary>
    /// <param name="workOrderService"></param>
    /// <param name="contentService"></param>
    /// <param name="userService"></param>
    /// <param name="kafkaMessenger"></param>
    /// <param name="kafkaOptions"></param>
    /// <param name="kafkaHubOptions"></param>
    /// <param name="serializerOptions"></param>
    public WorkOrderController(
        IWorkOrderService workOrderService,
        IContentService contentService,
        IUserService userService,
        IKafkaMessenger kafkaMessenger,
        IOptions<KafkaOptions> kafkaOptions,
        IOptions<KafkaHubConfig> kafkaHubOptions,
        IOptions<JsonSerializerOptions> serializerOptions)
    {
        _workOrderService = workOrderService;
        _contentService = contentService;
        _userService = userService;
        _kafkaMessenger = kafkaMessenger;
        _kafkaOptions = kafkaOptions.Value;
        _kafkaHubOptions = kafkaHubOptions.Value;
        _serializerOptions = serializerOptions.Value;
    }
    #endregion

    #region Endpoints
    /// <summary>
    /// Find a page of work orders for the specified query filter.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IPaged<WorkOrderMessageModel>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Tags = new[] { "WorkOrder" })]
    public IActionResult Find()
    {
        var uri = new Uri(this.Request.GetDisplayUrl());
        var query = Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery(uri.Query);
        var result = _workOrderService.Find(new WorkOrderFilter(query));
        var page = new Paged<WorkOrderMessageModel>(result.Items.Select(i => new WorkOrderMessageModel(i, _serializerOptions)), result.Page, result.Quantity, result.Total);
        return new JsonResult(page);
    }

    /// <summary>
    /// Request a transcript for the content for the specified 'contentId'.
    /// Publish message to kafka to request a transcription.
    /// </summary>
    /// <param name="contentId"></param>
    /// <returns></returns>
    [HttpPost("transcribe/{contentId}")]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(WorkOrderMessageModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Tags = new[] { "Content" })]
    public async Task<IActionResult> RequestTranscriptionAsync(long contentId)
    {
        var content = _contentService.FindById(contentId) ?? throw new InvalidOperationException("Content does not exist");
        if (String.IsNullOrWhiteSpace(_kafkaOptions.TranscriptionTopic)) throw new ConfigurationException("Kafka transcription topic not configured.");

        // Only allow one work order transcript request at a time.
        // TODO: Handle blocked work orders stuck in progress.
        var workOrders = _workOrderService.FindByContentId(contentId);
        if (workOrders.Any(o => o.WorkType == WorkOrderType.Transcription && _workLimiterStatus.Contains(o.Status)))
            return new JsonResult(new WorkOrderMessageModel(workOrders.First(o => o.WorkType == WorkOrderType.Transcription && _workLimiterStatus.Contains(o.Status)), _serializerOptions))
            {
                StatusCode = (int)HttpStatusCode.AlreadyReported
            };

        var username = User.GetUsername() ?? throw new NotAuthorizedException("Username is missing");
        var user = _userService.FindByUsername(username) ?? throw new NotAuthorizedException("User does not exist");
        var workOrder = _workOrderService.AddAndSave(new WorkOrder(WorkOrderType.Transcription, user, "", content));

        var result = await _kafkaMessenger.SendMessageAsync(_kafkaOptions.TranscriptionTopic, new TNO.Kafka.Models.TranscriptRequestModel(workOrder));
        if (result == null)
        {
            workOrder.Status = WorkOrderStatus.Failed;
            workOrder.Note = "Transcript request to Kafka failed";
            workOrder = _workOrderService.UpdateAndSave(workOrder);
            await _kafkaMessenger.SendMessageAsync(_kafkaHubOptions.HubTopic, new KafkaHubMessage(HubEvent.SendAll, new InvocationMessage("WorkOrder", new[] { new WorkOrderMessageModel(workOrder, _serializerOptions) })));
            throw new BadRequestException("Transcription request failed");
        }
        return new JsonResult(new WorkOrderMessageModel(workOrder, _serializerOptions));
    }
    #endregion
}