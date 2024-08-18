// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.ComponentModel.DataAnnotations;

namespace Cencora.TimeVault.WebApi.Models;

/// <summary>
/// Represents a location with city, country, postal code, and state or province information.
/// </summary>
public record LocationDto : IValidatableObject
{
    /// <summary>
    /// Gets or sets the city of the location.
    /// </summary>
    [Required]
    public required string City { get; init; }

    /// <summary>
    /// Gets or sets the country of the location.
    /// </summary>
    [Required]
    public required string Country { get; init; }

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
        var arr = new string?[]
        {
            City,
            StateOrProvince,
            PostalCode,
            Country,
        };

        return string.Join(", ", arr.Where(x => !string.IsNullOrWhiteSpace(x)));
    }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(City))
        {
            yield return new ValidationResult("The city is required.", [nameof(City)]);
        }

        if (string.IsNullOrWhiteSpace(Country))
        {
            yield return new ValidationResult("The country is required.", [nameof(Country)]);
        }
    }
}