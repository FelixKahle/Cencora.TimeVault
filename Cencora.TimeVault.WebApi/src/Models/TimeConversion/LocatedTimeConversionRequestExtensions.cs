// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using System.Globalization;
using Cencora.TimeVault.WebApi.Services.TimeConversion;

namespace Cencora.TimeVault.WebApi.Models.TimeConversion;

/// <summary>
/// Extension methods for <see cref="LocatedTimeConversionRequest"/>.
/// </summary>
public static class LocatedTimeConversionRequestExtensions
{
    /// <summary>
    /// Converts a <see cref="LocatedTimeConversionRequestDto"/> to a <see cref="LocatedTimeConversionRequest"/>.
    /// </summary>
    /// <param name="dto">The DTO to convert.</param>
    /// <returns>The converted request.</returns>
    public static LocatedTimeConversionRequest ToModel(this LocatedTimeConversionRequestDto dto)
    {
        return new LocatedTimeConversionRequest
        {
            OriginTime = DateTime.ParseExact(dto.OriginTime, dto.OriginTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None),
            OriginLocation = dto.OriginLocation.ToModel(),
            TargetLocation = dto.TargetLocation.ToModel(),
            ConvertedTimeFormat = dto.ConvertedTimeFormat,
            OriginResponseTimeFormat = dto.OriginResponseTimeFormat,
        };
    }

    /// <summary>
    /// Converts a <see cref="LocatedTimeConversionRequest"/> to a <see cref="LocatedTimeConversionInput"/>.
    /// </summary>
    /// <param name="request">The request to convert.</param>
    /// <returns>The converted input.</returns>
    public static LocatedTimeConversionInput ToInput(this LocatedTimeConversionRequest request)
    {
        return new LocatedTimeConversionInput
        {
            OriginTime = request.OriginTime,
            OriginLocation = request.OriginLocation,
            TargetLocation = request.TargetLocation,
        };
    }
}