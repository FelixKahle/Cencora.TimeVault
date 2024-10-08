// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Represents the input for a time conversion operation with locations.
/// </summary>
public readonly struct LocatedTimeConversionInput
{
    /// <summary>
    /// Gets or sets the origin time.
    /// </summary>
    public required DateTime OriginTime { get; init; }

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
        return $"{OriginTime} {OriginLocation} -> {TargetLocation}";
    }
}