// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models;

/// <summary>
/// Represents a location with city, country, postal code, and state or province information.
/// </summary>
public readonly struct Location
{
    /// <summary>
    /// Gets or sets the city of the location.
    /// </summary>
    public required string City { get; init; }

    /// <summary>
    /// Gets or sets the country of the location.
    /// </summary>
    public required string Country { get; init; }

    /// <summary>
    /// Gets or sets the postal code of the location.
    /// </summary>
    public required string PostalCode { get; init; }

    /// <summary>
    /// Gets or sets the state or province of the location.
    /// </summary>
    public required string StateOrProvince { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        var arr = new[] { City, StateOrProvince, PostalCode, Country };
        return string.Join(", ", arr.Where(s => !string.IsNullOrWhiteSpace(s)));
    }
}