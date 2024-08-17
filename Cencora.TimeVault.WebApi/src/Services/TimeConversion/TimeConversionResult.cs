// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// The result of a time conversion operation.
/// </summary>
public readonly struct TimeConversionResult
{
    /// <summary>
    /// Gets or sets the converted time.
    /// </summary>
    public required DateTime ConvertedTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time.
    /// </summary>
    public required DateTime OriginTime { get; init; }

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
        return $"{OriginTime} {OriginTimeZone.Id} -> {ConvertedTime} {TargetTimeZone.Id}";
    }
}