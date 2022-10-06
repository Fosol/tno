using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using TNO.API.Areas.Admin.Models.License;
using TNO.API.Models;
using TNO.DAL.Services;
using TNO.Entities.Models;

namespace TNO.API.Areas.Admin.Controllers;

/// <summary>
/// LicenseController class, provides License endpoints for the api.
/// </summary>
[Authorize]
[ApiController]
[Area("admin")]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[area]/licenses")]
[Route("api/[area]/licenses")]
[Route("v{version:apiVersion}/[area]/licenses")]
[Route("[area]/licenses")]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Unauthorized)]
[ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.Forbidden)]
public class LicenseController : ControllerBase
{
    #region Variables
    private readonly ILicenseService _service;
    #endregion

    #region Constructors
    /// <summary>
    /// Creates a new instance of a LicenseController object, initializes with specified parameters.
    /// </summary>
    /// <param name="service"></param>
    public LicenseController(ILicenseService service)
    {
        _service = service;
    }
    #endregion

    #region Endpoints
    /// <summary>
    /// Find a page of content for the specified query filter.
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(IPaged<LicenseModel>), (int)HttpStatusCode.OK)]
    [SwaggerOperation(Tags = new[] { "License" })]
    public IActionResult FindAll()
    {
        return new JsonResult(_service.FindAll().Select(ds => new LicenseModel(ds)));
    }

    /// <summary>
    /// Find content for the specified 'id'.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LicenseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.NoContent)]
    [SwaggerOperation(Tags = new[] { "License" })]
    public IActionResult FindById(int id)
    {
        var result = _service.FindById(id);

        if (result == null) return new NoContentResult();
        return new JsonResult(new LicenseModel(result));
    }

    /// <summary>
    /// Add content for the specified 'id'.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPost]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LicenseModel), (int)HttpStatusCode.Created)]
    [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Tags = new[] { "License" })]
    public IActionResult Add(LicenseModel model)
    {
        var result = _service.Add(model.ToEntity());
        return CreatedAtAction(nameof(FindById), new { id = result.Id }, new LicenseModel(result));
    }

    /// <summary>
    /// Update content for the specified 'id'.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LicenseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Tags = new[] { "License" })]
    public IActionResult Update(LicenseModel model)
    {
        var result = _service.Update(model.ToEntity());
        return new JsonResult(new LicenseModel(result));
    }

    /// <summary>
    /// Delete content for the specified 'id'.
    /// </summary>
    /// <param name="model"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(LicenseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponseModel), (int)HttpStatusCode.BadRequest)]
    [SwaggerOperation(Tags = new[] { "License" })]
    public IActionResult Delete(LicenseModel model)
    {
        _service.Delete(model.ToEntity());
        return new JsonResult(model);
    }
    #endregion
}