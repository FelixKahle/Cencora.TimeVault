// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;
using LanguageExt.Common;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// A service that provides time zone information.
/// </summary>
public interface ITimeZoneService
{
    /// <summary>
    /// Searches for a time zone for the specified location.
    /// </summary>
    /// <param name="location">The location to search for.</param>
    /// <returns>The result of the search.</returns>
    public Task<Result<TimeZoneInfo>> SearchTimeZoneAsync(Location location);
}