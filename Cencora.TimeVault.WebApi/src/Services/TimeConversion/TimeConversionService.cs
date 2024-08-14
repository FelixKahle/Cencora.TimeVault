// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

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
        _logger = logger;
    }

    /// <inheritdoc/>
    public TimeConversionResult ConvertTime(TimeConversionInput input)
    {
        var time = input.OriginTime;
        var originTimeZone = input.OriginTimeZone;
        var targetTimeZone = input.TargetTimeZone;
        
        // In case the two timezones are equal,
        // we do not need to perform any conversion.
        if (originTimeZone.Equals(targetTimeZone))
        {
            return new TimeConversionResult
            {
                ConvertedTime = time,
                OriginTime = time,
                OriginTimeZone = originTimeZone,
                TargetTimeZone = targetTimeZone
            };
        }

        // Perform the time conversion.
        var convertedTime = TimeZoneInfo.ConvertTime(time, originTimeZone, targetTimeZone);
        var result = new TimeConversionResult
        {
            ConvertedTime = convertedTime,
            OriginTime = time,
            OriginTimeZone = originTimeZone,
            TargetTimeZone = targetTimeZone
        };

        // Log some arbitrary information about the conversion.
        _logger.LogInformation($"Converted: {result}");

        return result;
    }
}