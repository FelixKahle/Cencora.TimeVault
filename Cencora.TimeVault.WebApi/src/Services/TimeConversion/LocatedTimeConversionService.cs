// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeZone;
using LanguageExt.Common;

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
    public async Task<Result<LocatedTimeConversionResult>> ConvertTimeAsync(LocatedTimeConversionInput input)
    {
        var originTimeZoneResult = await _timeZoneService.SearchTimeZoneAsync(input.OriginLocation);
        var targetTimeZoneResult = await _timeZoneService.SearchTimeZoneAsync(input.TargetLocation);

        return await originTimeZoneResult.Match(
            Succ: async originTimeZone =>
            {
                return await targetTimeZoneResult.Match(
                    Succ: async targetTimeZone =>
                    {
                        var timeConversionInput = new TimeConversionInput
                        {
                            OriginTime = input.OriginTime,
                            OriginTimeZone = originTimeZone,
                            TargetTimeZone = targetTimeZone
                        };

                        var conversionResult = await _timeConversionService.ConvertTimeAsync(timeConversionInput);
                        return conversionResult.Match(
                            Succ: result =>
                            {
                                var locatedTimeConversionResult = new LocatedTimeConversionResult
                                {
                                    ConvertedTime = result,
                                    OriginTimeZone = originTimeZone,
                                    TargetTimeZone = targetTimeZone,
                                };

                                return locatedTimeConversionResult;
                            },
                            Fail: error => new Result<LocatedTimeConversionResult>(error)
                        );
                    },
                    Fail: error => Task.FromResult(new Result<LocatedTimeConversionResult>(error))
                );
            },
            Fail: error => Task.FromResult(new Result<LocatedTimeConversionResult>(error))
        );
    }
}