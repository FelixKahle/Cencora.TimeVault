// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;
using LanguageExt;

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
    public required Option<TimeZoneInfo> TimeZone { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var timezoneId = TimeZone.Match(tz => tz.Id, () => "unknown");

        return $"{Location} -> {timezoneId}";
    }
}