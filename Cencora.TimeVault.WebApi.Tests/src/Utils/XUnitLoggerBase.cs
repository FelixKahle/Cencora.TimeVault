// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a logger implementation for XUnit tests.
/// </summary>
public abstract class XUnitLoggerBase
{
    [ThreadStatic]
    private static StringWriter _stringWriter = new();

    /// <summary>
    /// Gets or sets the string writer.
    /// </summary>
    protected StringWriter GetStringWriter()
    {
        return _stringWriter;
    }

    /// <summary>
    /// Resets the string writer.
    /// </summary>
    protected void ResetStringWriter()
    {
        var stringBuilder = _stringWriter.GetStringBuilder();
        stringBuilder.Clear();
        if (stringBuilder.Capacity > 1024)
        {
            stringBuilder.Capacity = 1024;
        }
    }
}