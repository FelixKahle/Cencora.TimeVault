// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Represents a factory for creating a value provider that converts query string parameter names to snake case.
/// </summary>
public class SnakeCaseQueryValueProviderFactory : IValueProviderFactory
{
    /// <summary>
    /// Creates a value provider for the specified context.
    /// </summary>
    /// <param name="context">The context to create the value provider for.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
    {
        ArgumentNullException.ThrowIfNull(context, nameof(context));

        var valueProvider = new SnakeCaseQueryValueProvider(
            BindingSource.Query,
            context.ActionContext.HttpContext.Request.Query,
            CultureInfo.CurrentCulture
        );

        context.ValueProviders.Add(valueProvider);

        return Task.CompletedTask;
    }
}