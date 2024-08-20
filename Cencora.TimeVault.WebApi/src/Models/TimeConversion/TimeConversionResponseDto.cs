// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Represents the response of a time conversion request.
/// </summary>
public record TimeConversionResponseDto
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

    /// <inheritdoc/>
    public override string ToString()
    {
        return
            $"{OriginTime}({OriginTimeFormat}) {OriginTimeZone} -> {ConvertedTime}({ConvertedTimeFormat}) {TargetTimeZone}";
    }
}