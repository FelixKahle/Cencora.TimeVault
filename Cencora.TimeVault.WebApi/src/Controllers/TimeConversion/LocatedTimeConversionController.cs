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

        var response = new LocatedTimeConversionResponse
        {
            ConvertedTime = result.ConvertedTime,
            ConvertedTimeFormat = model.ConvertedTimeFormat,
            OriginTime = result.OriginTime,
            OriginTimeFormat = model.OriginResponseTimeFormat,
            OriginTimeZone = result.OriginTimeZone,
            TargetTimeZone = result.TargetTimeZone,
            OriginLocation = model.OriginLocation,
            TargetLocation = model.TargetLocation,
        };

        return Ok(response.ToDto());
    }
}