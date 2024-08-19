// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Represents a request for a time zone.
/// </summary>
public readonly struct TimeZoneRequest
{
    /// <summary>
    /// Gets the location for the time zone request.
    /// </summary>
    public required Location Location { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return Location.ToString();
    }
}