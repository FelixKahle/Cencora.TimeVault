// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models;

/// <summary>
/// Represents a location with city, country, postal code, and state or province information.
/// </summary>
public record LocationDto
{
    /// <summary>
    /// Gets or sets the city of the location.
    /// </summary>
    public string? City { get; init; }

    /// <summary>
    /// Gets or sets the country of the location.
    /// </summary>
    public string? Country { get; init; }

    /// <summary>
    /// Gets or sets the postal code of the location.
    /// </summary>
    public string? PostalCode { get; init; }

    /// <summary>
    /// Gets or sets the state or province of the location.
    /// </summary>
    public string? StateOrProvince { get; init; }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{City}, {StateOrProvince}, {PostalCode}, {Country}";
    }
}