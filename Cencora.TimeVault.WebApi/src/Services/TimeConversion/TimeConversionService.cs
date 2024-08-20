// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using LanguageExt;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Default implementation of the time conversion service.
/// </summary>
public class TimeConversionService : ITimeConversionService
{
    private readonly ILogger<TimeConversionService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionService"/> class.
    /// </summary>
    /// <remarks>
    /// Sets the logger to a null logger.
    /// </remarks>
    public TimeConversionService()
    {
        _logger = NullLogger<TimeConversionService>.Instance;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public TimeConversionService(ILogger<TimeConversionService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));
        _logger = logger;
    }

    /// <inheritdoc/>
    public Task<TimeConversionResult> ConvertTimeAsync(TimeConversionInput input)
    {
        var time = input.OriginTime;
        var originTimeZone = input.OriginTimeZone;
        var targetTimeZone = input.TargetTimeZone;

        var tryConvertTime = new Try<TimeConversionResult>(() =>
        {
            var originDateTime = DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
            var convertedTime = TimeZoneInfo.ConvertTime(originDateTime, originTimeZone, targetTimeZone);
            return new TimeConversionResult
            {
                ConvertedTime = convertedTime,
                OriginTime = time,
                OriginTimeZone = originTimeZone,
                TargetTimeZone = targetTimeZone
            };
        });

        return tryConvertTime.Match(result =>
        {
            // Log some arbitrary information
            _logger.LogInformation("Converted {result}", result);
            return Task.FromResult(result);
        }, ex =>
        {
            var result = new TimeConversionResult
            {
                ConvertedTime = Option<DateTime>.None,
                OriginTime = time,
                OriginTimeZone = originTimeZone,
                TargetTimeZone = targetTimeZone
            };

            // Log the error
            _logger.LogError(ex, "Failed to convert {result}", result);
            return Task.FromResult(result);
        });
    }
}