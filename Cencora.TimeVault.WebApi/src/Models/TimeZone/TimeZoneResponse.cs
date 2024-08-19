// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Represents a response for a time zone.
/// </summary>
public readonly struct TimeZoneResponse
{
    /// <summary>
    /// Gets the location for the time zone response.
    /// </summary>
    public Location Location { get; init; }

    /// <summary>
    /// Gets the time zone for the location.
    /// </summary>
    public TimeZoneInfo TimeZone { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Location} - {TimeZone}";
    }
}