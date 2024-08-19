// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Extension methods for <see cref="TimeZoneRequest"/> and <see cref="TimeZoneRequestDto"/>.
/// </summary>
public static class TimeZoneRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeZoneRequestDto"/> to a <see cref="TimeZoneRequest"/>.
    /// </summary>
    /// <param name="dto">The DTO to convert.</param>
    /// <returns>The converted model.</returns>
    public static TimeZoneRequest ToModel(this TimeZoneRequestDto dto)
    {
        return new TimeZoneRequest
        {
            Location = dto.Location.ToModel()
        };
    }
}