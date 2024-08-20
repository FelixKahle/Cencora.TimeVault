// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// The result of a time conversion operation.
/// </summary>
public readonly struct LocatedTimeConversionResult
{
    /// <summary>
    /// Gets or sets the converted time.
    /// </summary>
    public required DateTime? ConvertedTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time.
    /// </summary>
    public required DateTime OriginTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time zone.
    /// </summary>
    public required TimeZoneInfo? OriginTimeZone { get; init; }

    /// <summary>
    /// Gets or sets the target time zone.
    /// </summary>
    public required TimeZoneInfo? TargetTimeZone { get; init; }

    /// <summary>
    /// Gets or sets the origin location.
    /// </summary>
    public required Location OriginLocation { get; init; }

    /// <summary>
    /// Gets or sets the target location.
    /// </summary>
    public required Location TargetLocation { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{OriginTime} {OriginLocation}, {OriginTimeZone?.Id} -> {ConvertedTime} {TargetLocation}, {TargetTimeZone?.Id}";
    }
}