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
    /// Gets or sets the IANA time zone ID.
    /// </summary>
    public required string IanaTimeZoneId { get; init; }

    /// <summary>
    /// Get or sets the Windows time zone ID.
    /// </summary>
    public required string WindowsTimeZoneId { get; init; }

    /// <summary>
    /// Gets or sets the Rails time zone IDs.
    /// </summary>
    public required List<string> RailsTimeZoneIds { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{Location} - {IanaTimeZoneId}, {WindowsTimeZoneId}, {string.Join(" | ", RailsTimeZoneIds)}";
    }
}