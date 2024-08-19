// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using TimeZoneConverter;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// Gets the type of the time zone.
/// </summary>
public enum TimeZoneType
{
    Iana,
    Windows,
    Rails,
    Unknown
}

/// <summary>
/// A helper class for time zones.
/// </summary>
public static class TimeZoneHelper
{
    /// <summary>
    /// Gets the type of the time zone based on the given time zone ID.
    /// </summary>
    /// <param name="timeZoneId">The time zone ID to get the type for.</param>
    /// <returns>The type of the time zone.</returns>
    public static TimeZoneType GetTimeZoneType(string timeZoneId)
    {
        if (string.IsNullOrWhiteSpace(timeZoneId))
        {
            return TimeZoneType.Unknown;
        }

        // Check if the timeZoneId is an IANA time zone
        if (TZConvert.TryIanaToWindows(timeZoneId, out _))
        {
            return TimeZoneType.Iana;
        }

        // Check if the timeZoneId is a Windows time zone
        if (TZConvert.TryWindowsToIana(timeZoneId, out _))
        {
            return TimeZoneType.Windows;
        }

        // Check if the timeZoneId is a Rails time zone
        if (TZConvert.TryRailsToIana(timeZoneId, out _))
        {
            return TimeZoneType.Rails;
        }

        return TimeZoneType.Unknown;
    }
}