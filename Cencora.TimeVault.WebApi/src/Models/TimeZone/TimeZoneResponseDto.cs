// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Represents a response for a time zone.
/// </summary>
public record TimeZoneResponseDto
{
    /// <summary>
    /// Gets the location for the time zone response.
    /// </summary>
    public required LocationDto Location { get; init; }

    /// <summary>
    /// Gets the time zone id for the time zone response.
    /// </summary>
    public required string TimeZoneId { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Location} - {TimeZoneId}";
    }
}