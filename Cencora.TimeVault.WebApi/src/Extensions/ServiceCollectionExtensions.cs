// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Configures the <see cref="JsonOptions"/> using the specified configuration.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <param name="configure">The configuration to apply.</param>
    /// <returns>The collection of services.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="configure"/> is <see langword="null"/>.</exception>
    public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services, Action<JsonOptions> configure)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));
        ArgumentNullException.ThrowIfNull(configure, nameof(configure));

        services.Configure<JsonOptions>(options =>
        {
            configure(options);
        });

        return services;
    }
}