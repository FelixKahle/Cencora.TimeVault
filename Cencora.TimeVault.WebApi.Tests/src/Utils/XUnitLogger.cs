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
    [ThreadStatic]
    // ReSharper disable once StaticMemberInGenericType
    private static StringWriter? _stringWriter;

    private readonly string _name;
    private readonly ITestOutputHelper _output;
    private readonly IXUnitFormatter _formatter;

    /// <summary>
    /// Gets or sets the scope provider.
    /// </summary>
    internal IExternalScopeProvider ScopeProvider { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="XUnitLogger{T}"/> class.
    /// </summary>
    public XUnitLogger(string name, IXUnitFormatter formatter, IExternalScopeProvider scopeProvider, ITestOutputHelper output)
    {
        _name = name;
        _formatter = formatter;
        ScopeProvider = scopeProvider;
        _output = output;
    }

    /// <inheritdoc/>
    public IDisposable BeginScope<TState>(TState state) where TState : notnull => ScopeProvider.Push(state);

    /// <inheritdoc/>
    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel != LogLevel.None;
    }

    /// <inheritdoc/>
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        ArgumentNullException.ThrowIfNull(formatter, nameof(formatter));
        
        if (!IsEnabled(logLevel))
        {
            return;
        }

        _stringWriter ??= new StringWriter();
        var logEntry = new LogEntry<TState>(logLevel, _name, eventId, state, exception, formatter);

        _formatter.Write(in logEntry, ScopeProvider, _stringWriter);

        var sb = _stringWriter.GetStringBuilder();

        if (sb.Length == 0)
        {
            return;
        }

        var computedAnsiString = sb.ToString();
        sb.Clear();

        if (sb.Capacity > 1024)
        {
            sb.Capacity = 1024;
        }

        _output.WriteLine(computedAnsiString);
    }
}