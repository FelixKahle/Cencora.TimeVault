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
    /// Uses the global exception handler for the application.
    /// </summary>
    /// <param name="app">The application builder.</param>
    /// <returns>The application builder.</returns>
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder app)
    {
        app.UseExceptionHandler();

        return app;
    }
}