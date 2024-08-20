// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models.TimeConversion;
using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Controllers.TimeConversion;

// TODO: Protect this controller with an authorization policy.

/// <summary>
/// Controller for time conversion based on the location of the origin and target.
/// </summary>
[ApiController]
[Route("time/conversion")]
public class LocatedTimeConversionController : ControllerBase
{
    private readonly ILocatedTimeConversionService _locatedTimeConversionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocatedTimeConversionController"/> class.
    /// </summary>
    /// <param name="locatedTimeConversionService">The located time conversion service.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="locatedTimeConversionService"/> is <see langword="null"/>.</exception>
    public LocatedTimeConversionController(ILocatedTimeConversionService locatedTimeConversionService)
    {
        ArgumentNullException.ThrowIfNull(locatedTimeConversionService, nameof(locatedTimeConversionService));

        _locatedTimeConversionService = locatedTimeConversionService;
    }

    /// <summary>
    /// Converts a time from one time zone to another based on the location of the origin and target.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpGet]
    [Consumes("application/json")]
    [Route("location")]
    public async Task<IActionResult> GetLocatedConvertTime([FromQuery] LocatedTimeConversionRequestDto request)
    {
        return await HandleRequest(request);
    }

    /// <summary>
    /// Converts a time from one time zone to another based on the location of the origin and target.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Route("location")]
    public async Task<IActionResult> PostLocatedConvertTime([FromBody] LocatedTimeConversionRequestDto request)
    {
        return await HandleRequest(request);
    }

    /// <summary>
    /// Processes a located time conversion request.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <returns>The handled request.</returns>
    private async Task<IActionResult> HandleRequest(LocatedTimeConversionRequestDto request)
    {
        var model = request.ToModel();
        var input = model.ToInput();
        var result = await _locatedTimeConversionService.ConvertTimeAsync(input);

        if (result.OriginTimeZone is null)
        {
            return Problem(
                detail: $"No time zone found for origin location {model.OriginLocation}.",
                statusCode: StatusCodes.Status404NotFound,
                title: "Time zone not found"
            );
        }

        if (result.TargetTimeZone is null)
        {
            return Problem(
                detail: $"No time zone found for target location {model.TargetLocation}.",
                statusCode: StatusCodes.Status404NotFound,
                title: "Time zone not found"
            );
        }

        if (result.ConvertedTime == null)
        {
            return Problem(
                detail: $"Could not convert time from {result.OriginTimeZone.Id} to {result.TargetTimeZone.Id}.",
                statusCode: StatusCodes.Status400BadRequest,
                title: "Time conversion failed"
            );
        }

        var response = new LocatedTimeConversionResponse
        {
            // It is safe to access the Value property here because the null check was done above.
            ConvertedTime = result.ConvertedTime.Value,
            ConvertedTimeFormat = model.ConvertedTimeFormat,
            OriginTime = result.OriginTime,
            OriginTimeFormat = model.OriginResponseTimeFormat,
            OriginTimeZone = result.OriginTimeZone,
            TargetTimeZone = result.TargetTimeZone,
            OriginLocation = result.OriginLocation,
            TargetLocation = result.TargetLocation
        };

        return Ok(response.ToDto());
    }
}