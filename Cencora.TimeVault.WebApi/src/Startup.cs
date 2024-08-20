// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Extensions;
using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Cencora.TimeVault.WebApi.Services.TimeZone;

namespace Cencora.TimeVault.WebApi;

// TODO: Use Authentication and Authorization to secure the API.

/// <summary>
/// Represents the startup class for the application.
/// </summary>
public sealed class Startup
{
    // ReSharper disable once UnusedAutoPropertyAccessor.Local
    private IConfiguration Configuration { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Startup"/> class.
    /// </summary>
    /// <param name="configuration">The configuration.</param>
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    /// <summary>
    /// Configures the services for the application.
    /// </summary>
    /// <param name="services">The collection of services to configure.</param>
    public void ConfigureServices(IServiceCollection services)
    {
        // Add services
        services.AddGlobalExceptionHandler();
        services.AddCustomLogging();
        services.AddControllers(options =>
        {
            // We want to add a route prefix to all controllers.
            options.Conventions.Add(new RoutePrefixConvention("api/v1"));
        });
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();

        // TimeVault services
        services.AddTimeZone();
        services.AddTimeConversion();

        // Configuration
        services.ConfigureJsonOptions();
    }

    /// <summary>
    /// Configures the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <param name="env">The web hosting environment.</param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseGlobalExceptionHandler();
        app.UseHttpsRedirection();
        app.UseStatusCodePages();

        if (env.IsDevelopment())
        {
            //app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}