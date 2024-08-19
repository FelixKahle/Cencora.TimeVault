// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;
using TimeZoneConverter;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// Provides methods for searching time zones.
/// </summary>
public class TimeZoneService : ITimeZoneService
{
    /// <inheritdoc/>
    public Task<SearchTimeZoneResult> SearchTimeZoneAsync(Location location)
    {
        var testResult = new SearchTimeZoneResult
        {
            Location = location,
            TimeZone = TZConvert.GetTimeZoneInfo("Europe/Berlin")
        };


        return Task.FromResult(testResult);
    }
}