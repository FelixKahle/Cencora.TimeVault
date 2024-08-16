// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Options for <see cref="SimpleXUnitFormatter"/>.
/// </summary>
/// <remarks>
/// The options are used to configure the behavior of <see cref="SimpleXUnitFormatter"/>.
/// </remarks>
public class SimpleXUnitFormatterOptions
{
    /// <summary>
    /// Includes scopes when <c>true</c>.
    /// </summary>
    public bool IncludeScopes { get; set; }

    /// <summary>
    /// Gets or sets format string used to format timestamp in logging messages. Defaults to <c>null</c>.
    /// </summary>
    [StringSyntax(StringSyntaxAttribute.DateTimeFormat)]
    public string? TimestampFormat { get; set; }

    /// <summary>
    /// Gets or sets indication whether or not UTC timezone should be used to format timestamps in logging messages. Defaults to <c>false</c>.
    /// </summary>
    public bool UseUtcTimestamp { get; set; }

    /// <summary>
    /// When <c>true</c>, the entire message gets logged in a single line.
    /// </summary>
    public bool SingleLine { get; set; }

    internal virtual void Configure(IConfiguration configuration) => configuration.Bind(this);
}