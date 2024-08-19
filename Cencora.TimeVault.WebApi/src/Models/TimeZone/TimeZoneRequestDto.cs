// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Represents a request for a time zone.
/// </summary>
public record TimeZoneRequestDto
{
    /// <summary>
    /// Gets or sets the location for the time zone request.
    /// </summary>
    public required LocationDto Location { get; set; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Location.ToString();
    }
}