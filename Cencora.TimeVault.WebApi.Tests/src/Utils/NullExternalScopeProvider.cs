// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Microsoft.Extensions.Logging;

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Scope provider that does nothing.
/// </summary>
internal sealed class NullExternalScopeProvider : IExternalScopeProvider
{
    /// <summary>
    /// Initializes a new instance of <see cref="NullExternalScopeProvider"/>.
    /// </summary>
    private NullExternalScopeProvider()
    {
    }

    /// <summary>
    /// Returns a cached instance of <see cref="NullExternalScopeProvider"/>.
    /// </summary>
    public static IExternalScopeProvider Instance { get; } = new NullExternalScopeProvider();

    /// <inheritdoc />
    void IExternalScopeProvider.ForEachScope<TState>(Action<object?, TState> callback, TState state)
    {
    }

    /// <inheritdoc />
    IDisposable IExternalScopeProvider.Push(object? state)
    {
        return NullScope.Instance;
    }
}