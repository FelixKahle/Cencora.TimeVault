// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Extension methods for <see cref="TimeZoneResponse"/>.
/// </summary>
public static class TimeZoneResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeZoneResponse"/> to a <see cref="TimeZoneResponseDto"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static TimeZoneResponseDto ToDto(this TimeZoneResponse response)
    {
        // This id might be a Windows time zone id, an IANA time zone id, or a Rails time zone id.
        var timeZoneId = response.TimeZone.Id;

        return new TimeZoneResponseDto
        {
            Location = response.Location.ToDto(),
            TimeZoneId = timeZoneId
        };
    }
}