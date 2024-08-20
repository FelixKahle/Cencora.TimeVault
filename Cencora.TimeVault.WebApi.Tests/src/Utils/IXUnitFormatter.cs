// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a log entry.
/// </summary>
public interface IXUnitFormatter
{
    /// <summary>
    /// Writes the specified log entry to the specified text writer.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <param name="logEntry">The log entry to write.</param>
    /// <param name="scopeProvider">The scope provider.</param>
    /// <param name="textWriter">The text writer to write to.</param>
    public void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider scopeProvider,
        TextWriter textWriter);
}