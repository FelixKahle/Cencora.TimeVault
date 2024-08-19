// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Cencora.TimeVault.WebApi.Extensions;
using TimeZoneConverter;

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// The input for a time conversion operation.
/// </summary>
public record TimeConversionRequestDto : IValidatableObject
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
    /// Gets or sets the origin time.
    /// </summary>
    [Required]
    public required string OriginTime { get; init; }

    /// <summary>
    /// Gets or sets the origin time format.
    /// </summary>
    /// <remarks>
    /// Defaults to ISO 8601.
    /// </remarks>
    public string OriginTimeFormat { get; init; } = DefaultOriginTimeFormat;

    /// <summary>
    /// Gets or sets the origin response time format.
    /// </summary>
    /// <remarks>
    /// This can be used if the origin time should be formatted differently in the response.
    /// The origin format may come in a different format than the target format,
    /// so this property can be used to specify the format for the response.
    /// </remarks>
    public string OriginResponseTimeFormat { get; init; } = DefaultOriginResponseTimeFormat;

    /// <summary>
    /// Gets or sets the origin time zone.
    /// </summary>
    [Required]
    public required string OriginTimeZone { get; init; }

    /// <summary>
    /// Gets or sets the target time zone.
    /// </summary>
    [Required]
    public required string TargetTimeZone { get; init; }

    /// <summary>
    /// Gets or sets the converted time format.
    /// </summary>
    public string ConvertedTimeFormat { get; init; } = DefaultConvertedTimeFormat;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionRequestDto"/> class.
    /// </summary>
    public TimeConversionRequestDto()
    {
    }

    /// <inheritdoc/>
    public override string ToString()
    {
        return $"{OriginTime}({OriginTimeFormat}) {OriginTimeZone} -> {TargetTimeZone}({ConvertedTimeFormat})";
    }

    /// <inheritdoc/>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        // Validate the origin time, time zone, and time format.
        if (string.IsNullOrWhiteSpace(OriginTime))
        {
            yield return new ValidationResult("The origin time is required.", [nameof(OriginTime)]);
        }
        if (string.IsNullOrWhiteSpace(OriginTimeZone))
        {
            yield return new ValidationResult("The origin time zone is required.", [nameof(OriginTimeZone)]);
        }
        if (string.IsNullOrWhiteSpace(OriginTimeFormat))
        {
            yield return new ValidationResult("The origin time format is required.", [nameof(OriginTimeFormat)]);
        }

        // Validate the target time strings.
        if (string.IsNullOrWhiteSpace(TargetTimeZone))
        {
            yield return new ValidationResult("The target time zone is required.", [nameof(TargetTimeZone)]);
        }
        if (string.IsNullOrWhiteSpace(ConvertedTimeFormat))
        {
            yield return new ValidationResult("The converted time format is required.", [nameof(ConvertedTimeFormat)]);
        }

        // Validate the time formats.
        if (OriginTimeFormat.IsValidDateTimeFormat() == false)
        {
            yield return new ValidationResult("The origin time format is not valid.", [nameof(OriginTimeFormat)]);
        }
        if (ConvertedTimeFormat.IsValidDateTimeFormat() == false)
        {
            yield return new ValidationResult("The converted time format is not valid.", [nameof(ConvertedTimeFormat)]);
        }
        if (OriginResponseTimeFormat.IsValidDateTimeFormat() == false)
        {
            yield return new ValidationResult("The origin response time format is not valid.", [nameof(OriginResponseTimeFormat)]);
        }

        // Check we can the origin time using the origin time format.
        if (DateTime.TryParseExact(OriginTime, OriginTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out _) == false)
        {
            yield return new ValidationResult($"The origin time is not in the specified format: {OriginTimeFormat}.", [nameof(OriginTime)]);
        }

        // Validate the time zones.
        if (TZConvert.TryGetTimeZoneInfo(OriginTimeZone, out _) == false)
        {
            yield return new ValidationResult("The origin time zone is not valid.", [nameof(OriginTimeZone)]);
        }
        if (TZConvert.TryGetTimeZoneInfo(TargetTimeZone, out _) == false)
        {
            yield return new ValidationResult("The target time zone is not valid.", [nameof(TargetTimeZone)]);
        }
    }
}