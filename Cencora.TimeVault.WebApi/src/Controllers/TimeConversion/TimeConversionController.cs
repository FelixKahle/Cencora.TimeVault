// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models.TimeConversion;
using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Controllers.TimeConversion;

// TODO: Protect this controller with an authorization policy.

/// <summary>
/// Controller for time conversion.
/// </summary>
[ApiController]
[Route("time/conversion")]
public class TimeConversionController : ControllerBase
{
    private readonly ITimeConversionService _timeConversionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionController"/> class.
    /// </summary>
    /// <param name="timeConversionService">The time conversion service.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="timeConversionService"/> is <see langword="null"/>.</exception>
    public TimeConversionController(ITimeConversionService timeConversionService)
    {
        ArgumentNullException.ThrowIfNull(timeConversionService,
            nameof(timeConversionService));
        _timeConversionService = timeConversionService;
    }

    [HttpGet]
    [Consumes("application/json")]
    [Route("timezone")]
    public async Task<IActionResult> GetConvertTime([FromQuery] TimeConversionRequestDto request)
    {
        return await HandleRequest(request);
    }

    /// <summary>
    /// Converts a time from one time zone to another.
    /// </summary>
    /// <param name="request">The request containing the time to convert.</param>
    /// <returns>The converted time.</returns>
    [HttpPost]
    [Consumes("application/json")]
    [Route("timezone")]
    public async Task<IActionResult> PostConvertTime([FromBody] TimeConversionRequestDto request)
    {
        return await HandleRequest(request);
    }

    /// <summary>
    /// Processes a time conversion request.
    /// </summary>
    /// <param name="request">The request to process.</param>
    /// <returns>The handled request.</returns>
    private async Task<IActionResult> HandleRequest(TimeConversionRequestDto request)
    {
        var model = request.ToModel();
        var input = model.ToInput();
        var result = await _timeConversionService.ConvertTimeAsync(input);

        return result.Match(
            Succ: dateTime =>
            {
                var response = new TimeConversionResponse
                {
                    ConvertedTime = dateTime,
                    ConvertedTimeFormat = model.ConvertedTimeFormat,
                    OriginTime = input.OriginTime,
                    OriginTimeFormat = model.OriginResponseTimeFormat,
                    OriginTimeZone = input.OriginTimeZone,
                    TargetTimeZone = input.TargetTimeZone
                };
                return Ok(response.ToDto());
            },
            Fail: error => CreateProblem(error)
        );
    }

    /// <summary>
    /// Creates a problem response for an exception.
    /// </summary>
    /// <param name="exception">The exception to create a problem response for.</param>
    /// <returns>The problem response.</returns>
    private ObjectResult CreateProblem(Exception exception)
    {
        return exception switch
        {
            TimeZoneNotFoundException timeZoneNotFoundException => Problem(
                detail: timeZoneNotFoundException.Message,
                statusCode: StatusCodes.Status404NotFound,
                title: "Time zone not found"
            ),
            TimeConversionException timeConversionException => Problem(
                detail: timeConversionException.Message,
                statusCode: StatusCodes.Status400BadRequest,
                title: "Time conversion failed"
            ),
            _ => Problem(
                statusCode: StatusCodes.Status500InternalServerError
            )
        };
    }
}