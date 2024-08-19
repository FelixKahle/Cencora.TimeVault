// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeZone;
using TimeZoneConverter;

namespace Cencora.TimeVault.WebApi.Models.TimeZone;

/// <summary>
/// Extension methods for <see cref="TimeZoneResponse"/>.
/// </summary>
public static class TimeZoneResponseExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeZoneResponse"/> to a <see cref="TimeZoneResponseDto"/>.
    /// </summary>
    /// <param name="response">The response to convert.</param>
    /// <returns>The converted response.</returns>
    public static TimeZoneResponseDto ToDto(this TimeZoneResponse response)
    {
        // This id might be a Windows time zone id, an IANA time zone id, or a Rails time zone id.
        string timeZoneId = response.TimeZone.Id;

        // We now convert the time zone id to IANA, Windows, and Rails time zone ids.
        var conversionResult = ConvertTimeZoneId(timeZoneId);

        return new TimeZoneResponseDto
        {
            Location = response.Location.ToDto(),
            IanaTimeZoneId = conversionResult.IanaTimeZoneId ?? "Unknown",
            WindowsTimeZoneId = conversionResult.WindowsTimeZoneId ?? "Unknown",
            RailsTimeZoneIds = conversionResult.RailsTimeZoneIds,
        };
    }

    /// <summary>
    /// Stores the conversion result of a time zone id.
    /// </summary>
    private readonly struct TimeZoneIdConversionResult
    {
        /// <summary>
        /// The IANA time zone ID.
        /// </summary>
        public string? IanaTimeZoneId { get; init; }

        /// <summary>
        /// The Windows time zone ID.
        /// </summary>
        public string? WindowsTimeZoneId { get; init; }

        /// <summary>
        /// The Rails time zone IDs.
        /// </summary>
        public List<string> RailsTimeZoneIds { get; init; }
    }

    /// <summary>
    /// Converts a time zone id to IANA, Windows, and Rails time zone ids.
    /// </summary>
    /// <param name="timeZoneId">The time zone id to convert.</param>
    /// <returns>The conversion result.</returns>
    /// <remarks>
    /// This method determines the type of the time zone id and converts it to IANA, Windows, and Rails time zone ids.
    /// If the time zone id is not recognized, all fields of the conversion result are set to <see langword="null"/>,
    /// and an empty list is returned for the Rails time zone ids.
    /// </remarks>
    private static TimeZoneIdConversionResult ConvertTimeZoneId(string timeZoneId)
    {
        try
        {
            var timeZoneType = TimeZoneHelper.GetTimeZoneType(timeZoneId);
            
            return timeZoneType switch
            {
                TimeZoneType.Iana => new TimeZoneIdConversionResult
                {
                    IanaTimeZoneId = timeZoneId,
                    WindowsTimeZoneId = TZConvert.IanaToWindows(timeZoneId),
                    RailsTimeZoneIds = TZConvert.IanaToRails(timeZoneId).ToList()
                },
                TimeZoneType.Windows => new TimeZoneIdConversionResult
                {
                    IanaTimeZoneId = TZConvert.WindowsToIana(timeZoneId),
                    WindowsTimeZoneId = timeZoneId,
                    RailsTimeZoneIds = TZConvert.IanaToRails(TZConvert.WindowsToIana(timeZoneId)).ToList()
                },
                TimeZoneType.Rails => new TimeZoneIdConversionResult
                {
                    IanaTimeZoneId = TZConvert.RailsToIana(timeZoneId),
                    WindowsTimeZoneId = TZConvert.IanaToWindows(TZConvert.RailsToIana(timeZoneId)),
                    RailsTimeZoneIds = new List<string> { timeZoneId }
                },
                _ => new TimeZoneIdConversionResult
                {
                    IanaTimeZoneId = null,
                    WindowsTimeZoneId = null,
                    RailsTimeZoneIds = new List<string>()
                }
            };
        }
        catch (Exception)
        {
            return new TimeZoneIdConversionResult
            {
                IanaTimeZoneId = null,
                WindowsTimeZoneId = null,
                RailsTimeZoneIds = new List<string>()
            };
        }
    }
}