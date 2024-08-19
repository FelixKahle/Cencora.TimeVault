// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Represents a response of a time conversion with location information.
/// </summary>
public readonly struct LocatedTimeConversionResponse
{
    /// <summary>
    /// Gets or sets the converted time.
    /// </summary>
    public required DateTime ConvertedTime { get; init; }

    /// <summary>
    /// Gets or sets the converted time format.
    /// </summary>
    public required string ConvertedTimeFormat { get; init; }

    /// <summary>
    /// Gets or sets the origin time.
    /// </summary>
    public required DateTime OriginTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time format.
    /// </summary>
    public required string OriginTimeFormat { get; init; }

    /// <summary>
    /// Gets or sets the origin time zone.
    /// </summary>
    public required TimeZoneInfo OriginTimeZone { get; init; }

    /// <summary>
    /// Gets or sets the target time zone.
    /// </summary>
    public required TimeZoneInfo TargetTimeZone { get; init; }

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
        return $"{OriginTime}({OriginTimeFormat}) {OriginLocation}, {OriginTimeZone.Id} -> {ConvertedTime}({ConvertedTimeFormat}) {TargetLocation}, {TargetTimeZone.Id}";
    }
}