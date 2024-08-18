// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Models;

/// <summary>
/// Provides extension methods for <see cref="Location"/> and <see cref="LocationDto"/>.
/// </summary>
public static class LocationExtensions
{
    /// <summary>
    /// Converts a <see cref="LocationDto"/> to a <see cref="Location"/>.
    /// </summary>
    /// <param name="locationDto">The location DTO to convert.</param>
    /// <returns>The converted location.</returns>
    public static Location ToModel(this LocationDto locationDto)
    {
        return new Location
        {
            City = locationDto.City ?? string.Empty,
            Country = locationDto.Country ?? string.Empty,
            PostalCode = locationDto.PostalCode ?? string.Empty,
            StateOrProvince = locationDto.StateOrProvince ?? string.Empty
        };
    }

    /// <summary>
    /// Converts a <see cref="Location"/> to a <see cref="LocationDto"/>.
    /// </summary>
    /// <param name="location">The location to convert.</param>
    /// <returns>The converted location DTO.</returns>
    public static LocationDto ToDto(this Location location)
    {
        return new LocationDto
        {
            City = location.City,
            Country = location.Country,
            PostalCode = location.PostalCode,
            StateOrProvince = location.StateOrProvince
        };
    }
}