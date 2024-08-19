// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Contains extension methods for <see cref="LocatedTimeConversionResponse"/>.
/// </summary>
public static class LocatedTimeConversionResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="LocatedTimeConversionResponse"/> to a <see cref="LocatedTimeConversionResponseDto"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static LocatedTimeConversionResponseDto ToDto(this LocatedTimeConversionResponse response)
    {
        var convertedTimeFormat = response.ConvertedTimeFormat;
        var originTimeFormat = response.OriginTimeFormat;

        return new LocatedTimeConversionResponseDto
        {
            ConvertedTime = response.ConvertedTime.ToString(convertedTimeFormat),
            ConvertedTimeFormat = convertedTimeFormat,
            OriginTime = response.OriginTime.ToString(originTimeFormat),
            OriginTimeFormat = originTimeFormat,
            OriginTimeZone = response.OriginTimeZone.Id,
            TargetTimeZone = response.TargetTimeZone.Id,
            OriginLocation = response.OriginLocation.ToDto(),
            TargetLocation = response.TargetLocation.ToDto()
        };
    }
}