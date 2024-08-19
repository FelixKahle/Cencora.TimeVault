// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeZone;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Service that converts a time from one time zone to another 
/// based on the location of the origin and target.
/// </summary>
public class LocatedTimeConversionService : ILocatedTimeConversionService
{
    // ReSharper disable once NotAccessedField.Local
    private readonly ITimeConversionService _timeConversionService;
    // ReSharper disable once NotAccessedField.Local
    private readonly ITimeZoneService _timeZoneService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocatedTimeConversionService"/> class.
    /// </summary>
    /// <param name="timeConversionService">The time conversion service.</param>
    /// <param name="timeZoneService">The time zone service.</param>
    /// <exception cref="ArgumentNullException">Thrown if <paramref name="timeConversionService"/> or <paramref name="timeZoneService"/> is <see langword="null"/>.</exception>
    public LocatedTimeConversionService(ITimeConversionService timeConversionService, ITimeZoneService timeZoneService)
    {
        ArgumentNullException.ThrowIfNull(timeConversionService, nameof(timeConversionService));
        ArgumentNullException.ThrowIfNull(timeZoneService, nameof(timeZoneService));

        _timeConversionService = timeConversionService;
        _timeZoneService = timeZoneService;
    }

    /// <inheritdoc/>
    public Task<LocatedTimeConversionResult> ConvertTimeAsync(LocatedTimeConversionInput input)
    {
        throw new NotImplementedException();
    }
}