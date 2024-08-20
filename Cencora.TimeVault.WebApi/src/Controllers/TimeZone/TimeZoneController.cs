// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models.TimeZone;
using Cencora.TimeVault.WebApi.Services.TimeZone;
using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Controllers.TimeZone;

// TODO: Protect this controller with an authorization policy.

/// <summary>
/// Controller for time zones.
/// </summary>
[ApiController]
[Route("timezone")]
public class TimeZoneController : ControllerBase
{
    private readonly ITimeZoneService _timeZoneService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZoneController"/> class.
    /// </summary>
    /// <param name="timeZoneService">The time zone service.</param>
    public TimeZoneController(ITimeZoneService timeZoneService)
    {
        ArgumentNullException.ThrowIfNull(timeZoneService, nameof(timeZoneService));
        _timeZoneService = timeZoneService;
    }

    /// <summary>
    /// Gets the time zone for a location.
    /// </summary>
    /// <param name="request">The request to get the time zone for.</param>
    /// <returns>The time zone for the location.</returns>
    [HttpGet]
    [Consumes("application/json")]
    public async Task<IActionResult> GetTimeZone([FromQuery] TimeZoneRequestDto request)
    {
        return await HandleTimeZoneRequest(request);
    }

    /// <summary>
    /// Posts the time zone for a location.
    /// </summary>
    /// <param name="request">The request to get the time zone for.</param>
    /// <returns>The time zone for the location.</returns>
    [HttpPost]
    [Consumes("application/json")]
    public async Task<IActionResult> PostTimeZone([FromBody] TimeZoneRequestDto request)
    {
        return await HandleTimeZoneRequest(request);
    }

    /// <summary>
    /// Handles the time zone request.
    /// </summary>
    /// <param name="request">The time zone request DTO.</param>
    /// <returns>The IActionResult with the time zone information.</returns>
    private async Task<IActionResult> HandleTimeZoneRequest(TimeZoneRequestDto request)
    {
        var model = request.ToModel();
        var result = await _timeZoneService.SearchTimeZoneAsync(model.Location);

        return result.TimeZone.Match(timeZone =>
            {
                var response = new TimeZoneResponse
                {
                    Location = result.Location,
                    TimeZone = timeZone
                };

                return Ok(response.ToDto());
            },
            () => Problem(
                detail: $"Could not find time zone for location {model.Location}.",
                statusCode: StatusCodes.Status500InternalServerError,
                title: "Time zone not found"
            ));
    }
}