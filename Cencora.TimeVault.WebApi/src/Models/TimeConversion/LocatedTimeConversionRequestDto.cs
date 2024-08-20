// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Cencora.TimeVault.WebApi.Extensions;

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Represents a request to convert a time from one time zone to another
/// based on the location of the origin and target.
/// </summary>
public record LocatedTimeConversionRequestDto : IValidatableObject
{
    /// <summary>
    /// The default origin time format.
    /// </summary>
    public const string DefaultOriginTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffK";

    /// <summary>
    /// The default origin response time format.
    /// </summary>
    public const string DefaultOriginResponseTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffK";

    /// <summary>
    /// The default converted time format.
    /// </summary>
    public const string DefaultConvertedTimeFormat = "yyyy-MM-ddTHH:mm:ss.fffK";

    /// <summary>
    /// Gets or initializes the origin time.
    /// </summary>
    [Required]
    public required string OriginTime { get; init; }

    /// <summary>
    /// Gets or initializes the origin time format.
    /// </summary>
    public string OriginTimeFormat { get; init; } = DefaultOriginTimeFormat;

    /// <summary>
    /// Gets or initializes the origin response time format.
    /// </summary>
    public string OriginResponseTimeFormat { get; init; } = DefaultOriginResponseTimeFormat;

    /// <summary>
    /// Gets or initializes the origin location.
    /// </summary>
    [Required]
    public required LocationDto OriginLocation { get; init; }

    /// <summary>
    /// Gets or initializes the target location.
    /// </summary>
    [Required]
    public required LocationDto TargetLocation { get; init; }

    /// <summary>
    /// Gets or initializes the converted time format.
    /// </summary>
    public string ConvertedTimeFormat { get; init; } = DefaultConvertedTimeFormat;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocatedTimeConversionRequestDto"/> class.
    /// </summary>
    public LocatedTimeConversionRequestDto()
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{OriginTime}({OriginTimeFormat}) {OriginLocation} -> {TargetLocation}({ConvertedTimeFormat})";
    }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (string.IsNullOrWhiteSpace(OriginTime))
            yield return new ValidationResult("The origin time is required.", [nameof(OriginTime)]);
        if (string.IsNullOrWhiteSpace(OriginTimeFormat))
            yield return new ValidationResult("The origin time format is required.", [nameof(OriginTimeFormat)]);

        if (string.IsNullOrWhiteSpace(ConvertedTimeFormat))
            yield return new ValidationResult("The converted time format is required.", [nameof(ConvertedTimeFormat)]);

        // Validate the time formats.
        if (OriginTimeFormat.IsValidDateTimeFormat() == false)
            yield return new ValidationResult("The origin time format is not valid.", [nameof(OriginTimeFormat)]);
        if (ConvertedTimeFormat.IsValidDateTimeFormat() == false)
            yield return new ValidationResult("The converted time format is not valid.", [nameof(ConvertedTimeFormat)]);
        if (OriginResponseTimeFormat.IsValidDateTimeFormat() == false)
            yield return new ValidationResult("The origin response time format is not valid.",
                [nameof(OriginResponseTimeFormat)]);

        // Check we can the origin time using the origin time format.
        if (DateTime.TryParseExact(OriginTime, OriginTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _) == false)
            yield return new ValidationResult($"The origin time is not in the specified format: {OriginTimeFormat}.",
                [nameof(OriginTime)]);
    }
}