// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

public sealed class SimpleXUnitFormatter : IXUnitFormatter
{
    private const string LogLevelPadding = ": ";
    private static readonly string MessagePadding = new(' ', GetLogLevelString(LogLevel.Information).Length + LogLevelPadding.Length);
    private static readonly string NewLineWithMessagePadding = Environment.NewLine + MessagePadding;

    private readonly SimpleXUnitFormatterOptions _loggerOptions;

    /// <summary>
    /// Initializes a new instance of <see cref="SimpleXUnitFormatter"/>.
    /// </summary>
    /// <param name="options">The options to configure the formatter.</param>
    public SimpleXUnitFormatter(SimpleXUnitFormatterOptions options)
    {
        _loggerOptions = options ?? throw new ArgumentNullException(nameof(options));
    }

    /// <inheritdoc/>
    public void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        var message = logEntry.Formatter(logEntry.State, logEntry.Exception);
        var logLevelString = GetLogLevelString(logEntry.LogLevel);

        WriteTimestamp(textWriter);
        textWriter.Write(logLevelString);

        WriteLogMessage(textWriter, logEntry, message, scopeProvider);
    }

    /// <summary>
    /// Creates and writes the log message to the text writer.
    /// </summary>
    private void WriteLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, IExternalScopeProvider? scopeProvider)
    {
        textWriter.Write(LogLevelPadding);
        textWriter.Write($"{logEntry.Category}[{logEntry.EventId.Id}]");

        if (!_loggerOptions.SingleLine)
        {
            textWriter.WriteLine();
        }

        WriteScopeInformation(textWriter, scopeProvider);
        WriteMessage(textWriter, message);

        if (logEntry.Exception == null)
        {
            return;
        }
        if (!_loggerOptions.SingleLine && !string.IsNullOrEmpty(message))
        {
            textWriter.WriteLine();
        }
        WriteMessage(textWriter, logEntry.Exception.ToString());
    }

    /// <summary>
    /// Writes the message to the text writer with appropriate formatting.
    /// </summary>
    private void WriteMessage(TextWriter textWriter, string message)
    {
        if (string.IsNullOrEmpty(message))
        {
            return;
        }
        textWriter.Write(_loggerOptions.SingleLine ? ' ' : MessagePadding);
        textWriter.Write(_loggerOptions.SingleLine
            ? message.Replace(Environment.NewLine, " ")
            : message.Replace(Environment.NewLine, NewLineWithMessagePadding));
    }

    /// <summary>
    /// Writes scope information to the text writer, if scopes are included.
    /// </summary>
    private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider? scopeProvider)
    {
        if (!_loggerOptions.IncludeScopes || scopeProvider == null)
        {
            return;
        }

        var isFirstScope = true;
        scopeProvider.ForEachScope((scope, writer) =>
        {
            if (isFirstScope)
            {
                writer.Write(MessagePadding + "=> ");
                isFirstScope = false;
            }
            else
            {
                writer.Write(" => ");
            }
            writer.Write(scope);
        }, textWriter);

        if (!isFirstScope && !_loggerOptions.SingleLine)
        {
            textWriter.WriteLine();
        }
    }

    /// <summary>
    /// Writes the timestamp to the text writer if the timestamp format is specified.
    /// </summary>
    private void WriteTimestamp(TextWriter textWriter)
    {
        if (_loggerOptions.TimestampFormat != null)
        {
            textWriter.Write(GetCurrentDateTime().ToString(_loggerOptions.TimestampFormat));
        }
    }

    /// <summary>
    /// Gets the current date and time based on the options.
    /// </summary>
    private DateTimeOffset GetCurrentDateTime()
    {
        return _loggerOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
    }

    /// <summary>
    /// Gets the string representation of the log level.
    /// </summary>
    private static string GetLogLevelString(LogLevel logLevel)
    {
        return logLevel switch
        {
            LogLevel.Trace => "trce",
            LogLevel.Debug => "dbug",
            LogLevel.Information => "info",
            LogLevel.Warning => "warn",
            LogLevel.Error => "fail",
            LogLevel.Critical => "crit",
            LogLevel.None => "none",
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
        };
    }
}