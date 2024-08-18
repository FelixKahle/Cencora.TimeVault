// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Contains extension methods for <see cref="TimeConversionResponse"/>.
/// </summary>
public static class TimeConversionResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeConversionResponse"/> to a <see cref="TimeConversionResponseDto"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static TimeConversionResponseDto ToDto(this TimeConversionResponse response)
    {
        var convertedTimeFormat = response.ConvertedTimeFormat;
        var originTimeFormat = response.OriginTimeFormat;

        return new TimeConversionResponseDto
        {
            ConvertedTime = response.ConvertedTime.ToString(convertedTimeFormat),
            ConvertedTimeFormat = convertedTimeFormat,
            OriginTime = response.OriginTime.ToString(originTimeFormat),
            OriginTimeFormat = originTimeFormat,
            OriginTimeZone = response.OriginTimeZone.Id,
            TargetTimeZone = response.TargetTimeZone.Id
        };
    }
}