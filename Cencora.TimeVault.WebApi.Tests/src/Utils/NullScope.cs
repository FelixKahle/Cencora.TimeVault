// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Tests.Utils;

/// <summary>
/// Represents a null scope.
/// </summary>
internal sealed class NullScope : IDisposable
{
    /// <summary>
    /// Gets a cached instance of <see cref="NullScope"/>.
    /// </summary>
    public static NullScope Instance { get; } = new NullScope();

    private NullScope()
    {
    }

    public void Dispose()
    {
    }
}