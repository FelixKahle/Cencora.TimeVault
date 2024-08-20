// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Extension methods for <see cref="ITestOutputHelper"/>.
/// </summary>
public static class TestOutputHelperExtensions
{
    /// <summary>
    /// Creates an instance of an XUnit logger for the specified type.
    /// </summary>
    /// <typeparam name="T">The type to create the logger for.</typeparam>
    /// <param name="output">The test output helper to write log messages to.</param>
    /// <param name="name">The name of the logger.</param>
    /// <param name="formatter">The formatter to use for log messages.</param>
    /// <param name="scopeProvider">The scope provider to use for log messages.</param>
    /// <returns>An instance of an XUnit logger.</returns>
    public static XUnitLogger<T> BuildLoggerFor<T>(this ITestOutputHelper output, string name,
        IXUnitFormatter formatter, IExternalScopeProvider scopeProvider)
    {
        return new XUnitLogger<T>(name, formatter, scopeProvider, output);
    }

    /// <summary>
    /// Creates an instance of an XUnit logger for the specified type.
    /// </summary>
    /// <typeparam name="T">The type to create the logger for.</typeparam>
    /// <param name="output">The test output helper to write log messages to.</param>
    /// <returns>An instance of an XUnit logger.</returns>
    public static ILogger<T> BuildLoggerFor<T>(this ITestOutputHelper output)
    {
        var name = typeof(T).FullName ?? typeof(T).Name;
        var options = SimpleXUnitFormatterOptions.Default;
        var formatter = new SimpleXUnitFormatter(options);
        var scopeProvider = NullExternalScopeProvider.Instance;

        return output.BuildLoggerFor<T>(name, formatter, scopeProvider);
    }
}