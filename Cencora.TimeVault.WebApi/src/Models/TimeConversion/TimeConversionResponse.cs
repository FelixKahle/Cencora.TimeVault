// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// The response for a time conversion operation.
/// </summary>
public readonly struct TimeConversionResponse
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

    /// <inheritdoc/>
    public override string ToString()
    {
        return
            $"{OriginTime}({OriginTimeFormat}) {OriginTimeZone.Id} -> {ConvertedTime}({ConvertedTimeFormat}) {TargetTimeZone.Id}";
    }
}