// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using LanguageExt.Common;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Default implementation of the time conversion service.
/// </summary>
public class TimeConversionService : ITimeConversionService
{
    /// <inheritdoc/>
    public Task<Result<DateTime>> ConvertTimeAsync(TimeConversionInput input)
    {
        var time = input.OriginTime;
        var originTimeZone = input.OriginTimeZone;
        var targetTimeZone = input.TargetTimeZone;

        try
        {
            var originDateTime = DateTime.SpecifyKind(time, DateTimeKind.Unspecified);
            var convertedTime = TimeZoneInfo.ConvertTime(originDateTime, originTimeZone, targetTimeZone);

            return Task.FromResult(new Result<DateTime>(convertedTime));
        }
        catch (Exception ex)
        {
            return Task.FromResult(new Result<DateTime>(
                new TimeConversionException(time, originTimeZone, targetTimeZone, ex))
            );
        }
    }
}