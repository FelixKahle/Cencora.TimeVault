// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;
using Cencora.TimeVault.WebApi.Models.TimeConversion;

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Extension methods for <see cref="TimeConversionRequest"/>.
/// </summary>
public static class TimeConversionRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="TimeConversionRequestDto"/> to a <see cref="TimeConversionRequest"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted request.</returns>
    public static TimeConversionRequest ToModel(this TimeConversionRequestDto request)
    {
        return new TimeConversionRequest
        {
            OriginTime = DateTime.ParseExact(request.OriginTime, request.OriginTimeFormat, CultureInfo.InvariantCulture),
            OriginTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.OriginTimeZone),
            TargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.TargetTimeZone),
            ConvertedTimeFormat = request.ConvertedTimeFormat,
            OriginTimeFormat = request.OriginTimeFormat,
            OriginResponseTimeFormat = request.OriginTimeFormat,
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeConversionRequest"/> to a <see cref="TimeConversionInput"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted input.</returns>
    public static TimeConversionInput ToInput(this TimeConversionRequest request)
    {
        return new TimeConversionInput
        {
            OriginTime = request.OriginTime,
            OriginTimeZone = request.OriginTimeZone,
            TargetTimeZone = request.TargetTimeZone,
        };
    }

    /// <summary>
    /// Converts a <see cref="TimeConversionRequest"/> to a <see cref="TimeConversionRequestDto"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted request.</returns>
    public static TimeConversionInput ToInput(this TimeConversionRequestDto request)
    {
        return new TimeConversionInput
        {
            OriginTime = DateTime.ParseExact(request.OriginTime, request.OriginTimeFormat, CultureInfo.InvariantCulture),
            OriginTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.OriginTimeZone),
            TargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById(request.TargetTimeZone),
        };
    }
}