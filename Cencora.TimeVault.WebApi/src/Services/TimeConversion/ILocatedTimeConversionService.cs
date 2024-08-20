// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using LanguageExt.Common;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Represents a service that converts a time from one time zone to another
/// based on the location of the origin and target.
/// </summary>
public interface ILocatedTimeConversionService
{
    /// <summary>
    /// Converts the time from the origin time zone to the target time zone
    /// based on the location of the origin and target.
    /// </summary>
    /// <param name="input">The input for the time conversion.</param>
    /// <returns>The result of the time conversion.</returns>
    Task<Result<LocatedTimeConversionResult>> ConvertTimeAsync(LocatedTimeConversionInput input);
}