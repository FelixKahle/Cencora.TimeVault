// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de


namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Service that converts a time from one time zone to another 
/// based on the location of the origin and target.
/// </summary>
public class LocatedTimeConversionService : ILocatedTimeConversionService
{
    // ReSharper disable once NotAccessedField.Local
    private readonly ITimeConversionService _timeConversionService;

    /// <summary>
    /// Initializes a new instance of the <see cref="LocatedTimeConversionService"/> class.
    /// </summary>
    /// <param name="timeConversionService">The time conversion service.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="timeConversionService"/> is <see langword="null"/>.</exception>
    public LocatedTimeConversionService(ITimeConversionService timeConversionService)
    {
        ArgumentNullException.ThrowIfNull(timeConversionService, nameof(timeConversionService));

        _timeConversionService = timeConversionService;
    }

    /// <inheritdoc/>
    public Task<LocatedTimeConversionResult> ConvertTimeAsync(in LocatedTimeConversionInput input)
    {
        throw new NotImplementedException();
    }
}