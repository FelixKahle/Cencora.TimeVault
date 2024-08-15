// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a base class for test loggers.
/// </summary>
public abstract class TestLoggerBase<TLogger>
{
    /// <summary>
    /// Gets the logger.
    /// </summary>
    protected ILogger<TLogger> Logger { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TestLoggerBase{TLogger}"/> class.
    /// </summary>
    /// <param name="output">The test output helper.</param>
    public TestLoggerBase(ITestOutputHelper output)
    {
        Logger = output.BuildLoggerFor<TLogger>();
    }
}