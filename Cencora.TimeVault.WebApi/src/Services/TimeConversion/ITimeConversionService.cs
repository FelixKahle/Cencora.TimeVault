// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using LanguageExt.Common;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// The service for converting times between time zones.
/// </summary>
public interface ITimeConversionService
{
    /// <summary>
    /// Converts the given time from the original time zone to the destination time zone.
    /// </summary>
    /// <param name="input">The input for the conversion.</param>
    /// <returns>The result of the conversion.</returns>
    Task<Result<DateTime>> ConvertTimeAsync(TimeConversionInput input);
}