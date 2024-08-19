// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// A service that provides time zone information.
/// </summary>
public interface ITimeZoneService
{
    /// <summary>
    /// Searches for a time zone based on the given location.
    /// </summary>
    /// <param name="location">The location to search for.</param>
    /// <returns>The result of the search.</returns>
    public Task<SearchTimeZoneResult> SearchTimeZoneAsync(Location location);
}