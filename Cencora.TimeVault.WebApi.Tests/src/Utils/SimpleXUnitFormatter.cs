// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Simple XUnit formatter for logging.
/// </summary>
public sealed class SimpleXUnitFormatter : IXUnitFormatter
{
    private const string LoglevelPadding = ": ";
    private static readonly string MessagePadding = new string(' ', GetLogLevelString(LogLevel.Information).Length + LoglevelPadding.Length);
    private static readonly string NewLineWithMessagePadding = Environment.NewLine + MessagePadding;

    private readonly SimpleXUnitFormatterOptions _loggerOptions;

    /// <summary>
    /// Initializes a new instance of <see cref="SimpleXUnitFormatter"/>.
    /// </summary>
    /// <param name="options">The options to configure the formatter.</param>
    public SimpleXUnitFormatter(SimpleXUnitFormatterOptions options)
    {
        _loggerOptions = options;
    }

    /// <inheritdoc/>
    public void Write<TState>(in LogEntry<TState> logEntry, IExternalScopeProvider? scopeProvider, TextWriter textWriter)
    {
        string message = logEntry.Formatter(logEntry.State, logEntry.Exception);

        LogLevel logLevel = logEntry.LogLevel;
        string logLevelString = GetLogLevelString(logLevel);

        string? timestamp = null;
        string? timestampFormat = _loggerOptions.TimestampFormat;

        if (timestampFormat != null)
        {
            DateTimeOffset dateTimeOffset = GetCurrentDateTime();
            timestamp = dateTimeOffset.ToString(timestampFormat);
        }

        if (timestamp != null)
        {
            textWriter.Write(timestamp);
            textWriter.Write(' ');
        }

        textWriter.Write(logLevelString);

        CreateDefaultLogMessage(textWriter, logEntry, message, scopeProvider);
    }

    /// <summary>
    /// Creates a default log message.
    /// </summary>
    /// <typeparam name="TState">The type of the state.</typeparam>
    /// <param name="textWriter">The text writer to write the log message to.</param>
    /// <param name="logEntry">The log entry to create the message for.</param>
    /// <param name="message">The message to log.</param>
    /// <param name="scopeProvider">The scope provider to use for log messages.</param>
    private void CreateDefaultLogMessage<TState>(TextWriter textWriter, in LogEntry<TState> logEntry, string message, IExternalScopeProvider? scopeProvider)
    {
        bool singleLine = _loggerOptions.SingleLine;
        int eventId = logEntry.EventId.Id;
        Exception? exception = logEntry.Exception;

        // Example:
        // info: ConsoleApp.Program[10]
        //       Request received

        // category and event id
        textWriter.Write(LoglevelPadding);
        textWriter.Write(logEntry.Category);
        textWriter.Write('[');

#if NET
        Span<char> span = stackalloc char[10];
        if (eventId.TryFormat(span, out int charsWritten))
        {
            textWriter.Write(span.Slice(0, charsWritten));
        }
        else
        {
            textWriter.Write(eventId.ToString());
        }
#else
        textWriter.Write(eventId.ToString());
#endif

        textWriter.Write(']');
        if (!singleLine)
        {
            textWriter.Write(Environment.NewLine);
        }

        // scope information
        WriteScopeInformation(textWriter, scopeProvider, singleLine);
        WriteMessage(textWriter, message, singleLine);

        // Example:
        // System.InvalidOperationException
        //    at Namespace.Class.Function() in File:line X
        if (exception != null)
        {
            if (!singleLine && !string.IsNullOrEmpty(message))
            {
                textWriter.Write(Environment.NewLine);
            }

            // exception message
            WriteMessage(textWriter, exception.ToString(), singleLine);
        }
    }

    /// <summary>
    /// Writes the message to the text writer.
    /// </summary>
    /// <param name="textWriter">The text writer to write the message to.</param>
    /// <param name="message">The message to write.</param>
    /// <param name="singleLine">Indicates whether the message should be written in a single line.</param>
    private static void WriteMessage(TextWriter textWriter, string message, bool singleLine)
    {
        if (!string.IsNullOrEmpty(message))
        {
            if (singleLine)
            {
                textWriter.Write(' ');
                WriteReplacing(textWriter, Environment.NewLine, " ", message);
            }
            else
            {
                textWriter.Write(MessagePadding);
                WriteReplacing(textWriter, Environment.NewLine, NewLineWithMessagePadding, message);
            }
        }

        static void WriteReplacing(TextWriter writer, string oldValue, string newValue, string message)
        {
            string newMessage = message.Replace(oldValue, newValue);
            writer.Write(newMessage);
        }
    }

    /// <summary>
    /// Gets the current date and time.
    /// </summary>
    private DateTimeOffset GetCurrentDateTime()
    {
        return _loggerOptions.UseUtcTimestamp ? DateTimeOffset.UtcNow : DateTimeOffset.Now;
    }

    /// <summary>
    /// Gets the log level string representation.
    /// </summary>
    /// <param name="logLevel">The log level to get the string representation for.</param>
    /// <returns>The log level string representation.</returns>
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
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel))
        };
    }

    /// <summary>
    /// Writes the scope information to the text writer.
    /// </summary>
    /// <param name="textWriter">The text writer to write the scope information to.</param>
    /// <param name="scopeProvider">The scope provider to use for log messages.</param>
    /// <param name="singleLine">Indicates whether the scope information should be written in a single line.</param>
    private void WriteScopeInformation(TextWriter textWriter, IExternalScopeProvider? scopeProvider, bool singleLine)
    {
        if (_loggerOptions.IncludeScopes && scopeProvider != null)
        {
            bool paddingNeeded = !singleLine;
            scopeProvider.ForEachScope((scope, state) =>
            {
                if (paddingNeeded)
                {
                    paddingNeeded = false;
                    state.Write(MessagePadding);
                    state.Write("=> ");
                }
                else
                {
                    state.Write(" => ");
                }

                state.Write(scope);
            }, textWriter);

            if (!paddingNeeded && !singleLine)
            {
                textWriter.Write(Environment.NewLine);
            }
        }
    }
}