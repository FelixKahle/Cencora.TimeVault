// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a logger implementation for XUnit tests.
/// </summary>
/// <typeparam name="T">The type to create a logger for.</typeparam>
public class XUnitLogger<T> : ILogger<T>
{
    private readonly string _name;
    private readonly ITestOutputHelper _output;
    private readonly IXUnitFormatter _formatter;

    /// <summary>
    /// Gets or sets the scope provider.
    /// </summary>
    internal IExternalScopeProvider ScopeProvider { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="XUnitLogger{T}"/> class.
    /// </summary>
    /// <param name="name">The name of the logger.</param>
    /// <param name="formatter">The formatter to use for log messages.</param>
    /// <param name="scopeProvider">The scope provider to use for log messages.</param>
    /// <param name="output">The test output helper to write log messages to.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="name"/>, <paramref name="formatter"/>, <paramref name="scopeProvider"/>, or <paramref name="output"/> is <see langword="null"/>.</exception>
    public XUnitLogger(string name, IXUnitFormatter formatter, IExternalScopeProvider scopeProvider,
        ITestOutputHelper output)
    {
        _name = name ?? throw new ArgumentNullException(nameof(name));
        _formatter = formatter ?? throw new ArgumentNullException(nameof(formatter));
        ScopeProvider = scopeProvider ?? throw new ArgumentNullException(nameof(scopeProvider));
        _output = output ?? throw new ArgumentNullException(nameof(output));
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull
    {
        return ScopeProvider.Push(state);
    }

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        ArgumentNullException.ThrowIfNull(formatter, nameof(formatter));

        if (!IsEnabled(logLevel)) return;

        using var stringWriter = new StringWriter();
        var logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);
        _formatter.Write(in logEntry, ScopeProvider, stringWriter);
        var sb = stringWriter.GetStringBuilder();

        // Nothing to log
        if (sb.Length == 0) return;

        var computedAnsiString = sb.ToString();
        _output.WriteLine(computedAnsiString);
    }
}