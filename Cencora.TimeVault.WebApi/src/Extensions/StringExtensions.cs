// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Extension methods for <see cref="String"/>
/// </summary>
public static class StringExtensions
{
    /// <summary>
    /// Determines whether the specified format is a valid date time format.
    /// </summary>
    /// <param name="format">The format to validate.</param>
    /// <returns><c>true</c> if the format is valid; otherwise, <c>false</c>.</returns>
    /// <remarks>
    /// Due to exceptions being thrown when parsing a date time string with an invalid format,
    /// this method can be slow for invalid formats.
    /// </remarks>
    public static bool IsValidDateTimeFormat(this string format)
    {
        if (string.IsNullOrWhiteSpace(format))
        {
            return false;
        }

        try
        {
            // Generate a sample date and time string based on the provided format
            var sampleDateTime = DateTime.UtcNow.ToString(format, CultureInfo.InvariantCulture);

            // Try to parse the sample date and time string using the provided format
            return DateTime.TryParseExact(sampleDateTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out _);
        }
        catch (FormatException)
        {
            return false;
        }
    }

    /// <summary>
    /// Checks whether all strings in the specified collection are <c>null</c> or white space.
    /// </summary>
    /// <param name="values">The collection of strings to check.</param>
    /// <returns><c>true</c> if all strings are <c>null</c> or white space; otherwise, <c>false</c>.</returns>
    public static bool AllNullOrWhiteSpace(this IEnumerable<string?> values)
    {
        return values.All(string.IsNullOrWhiteSpace);
    }
}