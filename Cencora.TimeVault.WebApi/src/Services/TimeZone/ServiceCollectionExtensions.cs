// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the time zone service to the service collection.
    /// </summary>
    /// <param name="services">The service collection to add the time zone service to.</param>
    /// <returns>The service collection with the time zone service added.</returns>
    public static IServiceCollection AddTimeZone(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<ITimeZoneService, TimeZoneService>();
        return services;
    }
}