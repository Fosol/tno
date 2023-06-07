using System.Net;
using System.Net.Mime;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TNO.API.Areas.Subscriber.Models.Minister;
using TNO.API.Filters;
using TNO.API.Models;
using TNO.DAL.Services;
using TNO.Keycloak;

namespace TNO.API.Areas.Subscriber.Controllers;

/// <summary>
/// ContributorController class, provides Minister endpoints for the api.
/// </summary>
[ClientRoleAuthorize(ClientRole.Subscriber, ClientRole.Administrator)]
[ApiController]
[Area("subscriber")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[area]/ministers")]
[Route("api/[area]/ministers")]
[Route("v{version:apiVersion}/[area]/ministers")]
[Route("[area]/ministers")]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Unauthorized)]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Forbidden)]
public class MinisterController : ControllerBase
{
    #region Variables
    private readonly IMinisterService _service;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of a ContributorController object, initializes with specified parameters.
    /// </summary>
    /// <param name="service"></param>
    public MinisterController(IMinisterService service)
    {
        _service = service;
    }
    #endregion

    #region Endpoints
    /// <summary>
    /// Return an array of Minister.
    /// </summary>
    /// <returns></returns>
    [HttpGet, HttpHead]
    [Produces(MediaTypeNames.Application.Json)]
    [ProducesResponseType(typeof(IEnumerable<MinisterModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotModified)]
    [SwaggerOperation(Tags = new[] { "Minister" })]
    [ETagCacheTableFilter("minister")]
    [ResponseCache(Duration = 5 * 60)]
    public IActionResult FindAll()
    {
        return new JsonResult(_service.FindAll());
    }
    #endregion
}