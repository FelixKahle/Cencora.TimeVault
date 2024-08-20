// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeZone;
using LanguageExt;

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
    public LocatedTimeConversionService(ITimeConversionService timeConversionService,
        ITimeZoneService timeZoneService)
    {
        ArgumentNullException.ThrowIfNull(timeConversionService,
            nameof(timeConversionService));
        ArgumentNullException.ThrowIfNull(timeZoneService,
            nameof(timeZoneService));

        _timeConversionService = timeConversionService;
        _timeZoneService = timeZoneService;
    }

    /// <inheritdoc/>
    public async Task<LocatedTimeConversionResult> ConvertTimeAsync(LocatedTimeConversionInput input)
    {
        var originTimeZoneResult = await _timeZoneService.SearchTimeZoneAsync(input.OriginLocation);
        var targetTimeZoneResult = await _timeZoneService.SearchTimeZoneAsync(input.TargetLocation);

        var originTimeZone = originTimeZoneResult.TimeZone;
        var targetTimeZone = targetTimeZoneResult.TimeZone;

        return await originTimeZone.Match(
            originTz => targetTimeZone.Match(
                async targetTz =>
                {
                    var timeConversionInput = new TimeConversionInput
                    {
                        OriginTime = input.OriginTime,
                        OriginTimeZone = originTz,
                        TargetTimeZone = targetTz
                    };

                    var convertedTime = await _timeConversionService.ConvertTimeAsync(timeConversionInput);

                    return new LocatedTimeConversionResult
                    {
                        ConvertedTime = convertedTime.ConvertedTime,
                        OriginTime = input.OriginTime,
                        OriginLocation = input.OriginLocation,
                        TargetLocation = input.TargetLocation,
                        OriginTimeZone = originTz,
                        TargetTimeZone = targetTz
                    };
                },
                () => Task.FromResult(new LocatedTimeConversionResult
                {
                    ConvertedTime = Option<DateTime>.None,
                    OriginTime = input.OriginTime,
                    OriginLocation = input.OriginLocation,
                    TargetLocation = input.TargetLocation,
                    OriginTimeZone = originTimeZone,
                    TargetTimeZone = targetTimeZone
                })
            ),
            () => Task.FromResult(new LocatedTimeConversionResult
            {
                ConvertedTime = Option<DateTime>.None,
                OriginTime = input.OriginTime,
                OriginLocation = input.OriginLocation,
                TargetLocation = input.TargetLocation,
                OriginTimeZone = originTimeZone,
                TargetTimeZone = targetTimeZone
            })
        );
    }
}