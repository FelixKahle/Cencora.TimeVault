// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Extension methods for <see cref="IApplicationBuilder"/>.
/// </summary>
public static class ApplicationBuilderExtensions
{
    /// <summary>
    /// The title of the Swagger UI.
    /// </summary>
    public const string SwaggerTitle = "TimeVault API";

    /// <summary>
    /// Uses the global exception handler for the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="app"/> is <see langword="null"/>.</exception>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        app.UseExceptionHandler();
        return app;
    }

    /// <summary>
    /// Uses the custom Swagger UI for the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="app"/> is <see langword="null"/>.</exception>
    public static IApplicationBuilder UseCustomSwaggerUi(this IApplicationBuilder app)
    {
        ArgumentNullException.ThrowIfNull(app, nameof(app));

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.DocumentTitle = SwaggerTitle;
        });
        return app;
    }
}