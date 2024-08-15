// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a logger implementation for XUnit tests.
/// </summary>
/// <typeparam name="T">The type to create a logger for.</typeparam>
public class XUnitLogger<T> : ILogger<T>, IDisposable
{
    private ITestOutputHelper _output;
        
    /// <summary>
    /// Initializes a new instance of the <see cref="XUnitLogger{T}"/> class.
    /// </summary>
    /// <param name="output">The test output helper.</param>
    public XUnitLogger(ITestOutputHelper output)
    {
        _output = output;
    }

    /// <inheritdoc/>
    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return this;
    }

    /// <inheritdoc/>
    public void Dispose()
    {
    }

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
    {
        return true;
    }

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        _output.WriteLine(state?.ToString() ?? string.Empty);
    }
}