// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Represents a request to convert a time from one time zone to another
/// based on the location of the origin and target time zones.
/// </summary>
public readonly struct LocatedTimeConversionRequest
{
    /// <summary>
    /// Gets or sets the origin time.
    /// </summary>
    public required DateTime OriginTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time zone.
    /// </summary>
    public required Location OriginLocation { get; init; }

    /// <summary>
    /// Gets or sets the target time zone.
    /// </summary>
    public required Location TargetLocation { get; init; }

    /// <summary>
    /// Gets or sets the converted time format.
    /// </summary>
    public required string ConvertedTimeFormat { get; init; }

    /// <summary>
    /// Gets or sets the origin response time format.
    /// </summary>
    /// <remarks>
    /// This can be used if the origin time should be formatted differently in the response.
    /// The origin format may come in a different format than the target format,
    /// so this property can be used to specify the format for the response.
    /// </remarks>
    public required string OriginResponseTimeFormat { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{OriginTime} {OriginLocation} -> {TargetLocation} ({ConvertedTimeFormat})";
    }
}