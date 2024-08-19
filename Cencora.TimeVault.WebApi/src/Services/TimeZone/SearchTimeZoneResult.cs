// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// Represents the result of a time zone search.
/// </summary>
public readonly struct SearchTimeZoneResult
{
    /// <summary>
    /// Gets the location for the searched time zone.
    /// </summary>
    public required Location Location { get; init; }

    /// <summary>
    /// Gets the time zone information.
    /// </summary>
    /// <remarks>
    /// This property may be <see langword="null"/> if the time zone could not be found.
    /// </remarks>
    public required TimeZoneInfo? TimeZone { get; init; }

    /// <summary>
    /// Gets a value indicating whether the time zone was found.
    /// </summary>
    public bool IsFound => TimeZone is not null;

    /// <inheritdoc/>
    public override string ToString()
    {
        return TimeZone is not null
            ? $"{TimeZone.Id} ({Location})"
            : $"Not found ({Location})";
    }
}