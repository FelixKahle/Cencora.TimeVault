// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Extension methods for <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Adds custom logging configuration to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        services.AddLogging(builder => builder.Configure(options =>
        {
            options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId | ActivityTrackingOptions.TraceId | ActivityTrackingOptions.Tags;
        }));
        return services;
    }

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

        services.Configure(configure);

        return services;
    }

    /// <summary>
    /// Configures the <see cref="JsonOptions"/> using the default configuration.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.ConfigureJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower;
            options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
            options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
            options.JsonSerializerOptions.AllowTrailingCommas = true;
        });

        return services;
    }

    /// <summary>
    /// Adds HTTP logging to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    public static IServiceCollection AddHttpLogging(this IServiceCollection services)
    {
        services.AddHttpLogging(_ => { });
        return services;
    }

    /// <summary>
    /// Adds a global exception handler to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
            };
        });
        return services;
    }
}