// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// Provides methods for searching time zones.
/// </summary>
public class TimeZoneService : ITimeZoneService
{
    private readonly ILogger<TimeZoneService> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeZoneService"/> class.
    /// </summary>
    /// <param name="logger">The logger.</param>
    public TimeZoneService(ILogger<TimeZoneService> logger)
    {
        ArgumentNullException.ThrowIfNull(logger, nameof(logger));

        _logger = logger;
    }

    /// <inheritdoc/>
    public Task<SearchTimeZoneResult> SearchTimeZoneAsync(Location location)
    {
        throw new NotImplementedException();
    }
}