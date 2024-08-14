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
    /// Adds the <see cref="ITimeConversionService"/> to the service collection.
    /// </summary>
    /// <remarks>
    /// The <see cref="ITimeConversionService"/> is added as a transient service.
    /// </remarks>
    /// <typeparam name="TConversion">The type of the <see cref="ITimeConversionService"/> implementation.</typeparam>
    /// <param name="services">The <see cref="IServiceCollection"/>.</param>
    /// <returns>The <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddTimeConversion<TConversion>(this IServiceCollection services)
        where TConversion : class, ITimeConversionService
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddTransient<ITimeConversionService, TConversion>();

        return services;
    }

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

        services.AddTimeConversion<TimeConversionService>();

        return services;
    }
}