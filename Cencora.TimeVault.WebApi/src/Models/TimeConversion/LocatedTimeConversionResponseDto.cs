// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Represents a response of a time conversion with location information.
/// </summary>
public record LocatedTimeConversionResponseDto
{
    /// <summary>
    /// The converted time.
    /// </summary>
    public required string ConvertedTime { get; init; }

    /// <summary>
    /// The format of the converted time.
    /// </summary>
    public required string ConvertedTimeFormat { get; init; }

    /// <summary>
    /// The original time.
    /// </summary>
    public required string OriginTime { get; init; }

    /// <summary>
    /// The format of the original time.
    /// </summary>
    public required string OriginTimeFormat { get; init; }

    /// <summary>
    /// The original time zone.
    /// </summary>
    public required string OriginTimeZone { get; init; }

    /// <summary>
    /// The target time zone.
    /// </summary>
    public required string TargetTimeZone { get; init; }

    /// <summary>
    /// The origin location.
    /// </summary>
    public required LocationDto OriginLocation { get; init; }

    /// <summary>
    /// The target location.
    /// </summary>
    public required LocationDto TargetLocation { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return
            $"{OriginTime}({OriginTimeFormat}) {OriginLocation}, {OriginTimeZone} -> {ConvertedTime}({ConvertedTimeFormat}) {TargetLocation}, {TargetTimeZone}";
    }
}