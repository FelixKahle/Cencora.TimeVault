// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models.TimeConversion;
using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Controllers;

// TODO: Protect this controller with an authorization policy.

/// <summary>
/// Controller for time conversion.
/// </summary>
[ApiController]
[Route("api/v1/time/conversion")]
public class TimeConversionController : ControllerBase
{
    private readonly ITimeConversionService _timeConversionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionController"/> class.
    /// </summary>
    /// <param name="timeConversionService">The time conversion service.</param>
    public TimeConversionController(ITimeConversionService timeConversionService)
    {
        _timeConversionService = timeConversionService;
    }

    [HttpGet]
    [Consumes("application/json")]
    [Route("timezone")]
    public IActionResult GetConvertTime([FromQuery] TimeConversionRequestDto request)
    {
        var response = ProcessRequest(request);
        return Ok(response.ToDto());
    }

    /// <summary>
    /// Converts a time from one time zone to another.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Route("timezone")]
    public IActionResult PostConvertTime([FromBody] TimeConversionRequestDto request)
    {
        var response = ProcessRequest(request);
        return Ok(response.ToDto());
    }

    /// <summary>
    /// Converts a time from one time zone to another based on the location of the origin and target.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpGet]
    [Consumes("application/json")]
    [Route("location")]
    public IActionResult GetLocatedConvertTime([FromQuery] LocatedTimeConversionRequest request)
    {
        return Ok();
    }

    /// <summary>
    /// Converts a time from one time zone to another based on the location of the origin and target.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Route("location")]
    public IActionResult PostLocatedConvertTime([FromBody] LocatedTimeConversionRequest request)
    {
        return Ok();
    }

    /// <summary>
    /// Processes a time conversion request.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <returns>The response to the request.</returns>
    private TimeConversionResponse ProcessRequest(TimeConversionRequestDto request)
    {
        var model = request.ToModel();
        var input = model.ToInput();
        var result = _timeConversionService.ConvertTime(input);
        
        var response = new TimeConversionResponse
        {
            ConvertedTime = result.ConvertedTime,
            ConvertedTimeFormat = model.ConvertedTimeFormat,
            OriginTime = result.OriginTime,
            OriginTimeFormat = model.OriginResponseTimeFormat,
            OriginTimeZone = result.OriginTimeZone,
            TargetTimeZone = result.TargetTimeZone,
        };

        return response;
    }
}