// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds the <see cref="TimeConversionService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// The <see cref="TimeConversionService"/> is added as a transient service.
    /// </remarks>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddTimeConversion(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<ITimeConversionService, TimeConversionService>();
        services.AddTransient<ILocatedTimeConversionService, LocatedTimeConversionService>();

        return services;
    }
}