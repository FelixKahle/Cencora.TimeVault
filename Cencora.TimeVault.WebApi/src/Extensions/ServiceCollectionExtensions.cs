// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public static IServiceCollection AddCustomLogging(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddLogging(builder => builder.Configure(options =>
        {
            options.ActivityTrackingOptions = ActivityTrackingOptions.SpanId | ActivityTrackingOptions.TraceId |
                                              ActivityTrackingOptions.Tags;
        }));
        return services;
    }

    /// <summary>
    /// Adds custom Swagger generation to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public static IServiceCollection AddCustomSwaggerGen(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "TimeVault API",
                Version = "v1",
                Description = 
                    @"TimeVault is a service tailored to store, 
                    manage, and retrieve timezone data for specific locations or 
                    geographical coordinates. By leveraging this service, applications 
                    can seamlessly perform accurate time translations between various timezones, 
                    which is crucial for global scheduling, event management, and time-sensitive 
                    operations.".RemoveNewLines()
            });
            options.OperationFilter<SnakeCaseParamOperationFilter>();
        });
        return services;
    }

    /// <summary>
    /// Configures the <see cref="JsonOptions"/> using the specified configuration.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <param name="configure">The configuration to apply.</param>
    /// <returns>The collection of services.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> or <paramref name="configure"/> is <see langword="null"/>.</exception>
    public static IServiceCollection ConfigureJsonOptions(this IServiceCollection services,
        Action<JsonOptions> configure)
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
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public static IServiceCollection AddHttpLogging(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddHttpLogging(_ => { });
        return services;
    }

    /// <summary>
    /// Adds a global exception handler to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public static IServiceCollection AddGlobalExceptionHandler(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddProblemDetails(options =>
        {
            options.CustomizeProblemDetails = context =>
            {
                context.ProblemDetails.Extensions.TryAdd("traceId", context.HttpContext.TraceIdentifier);
            };
        });
        return services;
    }

    /// <summary>
    /// Adds custom controllers to the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    /// <returns>The collection of services.</returns>
    /// <remarks>
    /// This method adds a custom <see cref="SnakeCaseQueryValueProviderFactory"/> to the list of value provider factories
    /// and configures the controllers to use the route prefix "api/v1".
    /// </remarks>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="services"/> is <see langword="null"/>.</exception>
    public static IServiceCollection AddCustomControllers(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services, nameof(services));

        services.AddControllers(options =>
        {
            options.Conventions.Add(new RoutePrefixConvention("api/v1"));
            options.ValueProviderFactories.Add(new SnakeCaseQueryValueProviderFactory());
        });
        return services;
    }
}