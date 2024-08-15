// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Utility class for creating XUnit loggers.
/// </summary>
public static class XUnitLoggerUtilities
{
    /// <summary>
    /// Creates an instance of an XUnit logger for the specified type.
    /// </summary>
    /// <typeparam name="T">The type to create the logger for.</typeparam>
    /// <param name="output">The test output helper to write log messages to.</param>
    /// <returns>An instance of an XUnit logger.</returns>
    public static ILogger<T> CreateLogger<T>(ITestOutputHelper output)
    {
        return new XUnitLogger<T>(output);
    }
}