// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cencora.TimeVault.WebApi.Extensions;

/// <summary>
/// Provides a value provider for query string values with snake case naming convention.
/// </summary>
public class SnakeCaseQueryValueProvider : QueryStringValueProvider
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SnakeCaseQueryValueProvider"/> class.
    /// </summary>
    /// <param name="bindingSource">The binding source.</param>
    /// <param name="values">The query string values.</param>
    /// <param name="culture">The culture to use for formatting.</param>
    public SnakeCaseQueryValueProvider(BindingSource bindingSource, IQueryCollection values, CultureInfo culture)
        : base(bindingSource, values, culture)
    {
    }

    /// <summary>
    /// Determines whether the value provider contains a value with the specified prefix.
    /// </summary>
    /// <param name="prefix">The prefix to search for.</param>
    /// <returns><c>true</c> if the value provider contains a value with the specified prefix; otherwise, <c>false</c>.</returns>
    public override bool ContainsPrefix(string prefix)
    {
        return base.ContainsPrefix(prefix.ToSnakeCase());
    }

    /// <summary>
    /// Gets the value with the specified key from the value provider.
    /// </summary>
    /// <param name="key">The key of the value to get.</param>
    /// <returns>A <see cref="ValueProviderResult"/> object containing the value.</returns>
    public override ValueProviderResult GetValue(string key)
    {
        return base.GetValue(key.ToSnakeCase());
    }
}