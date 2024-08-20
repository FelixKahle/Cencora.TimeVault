// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Cencora.TimeVault.WebApi.Tests.Utils;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.Services.TimeConversion;

/// <summary>
/// Represents the test class for the <see cref="TimeConversionService"/> class.
/// </summary>
public class TimeConversionServiceTests : TestLoggerBase<TimeConversionService>
{
    /// <summary>
    /// The toleration for time comparisons.
    /// </summary>
    public static readonly TimeSpan Toleration = TimeSpan.FromMilliseconds(100);

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionServiceTests"/> class.
    /// </summary>
    /// <param name="output">The test output helper.</param>
    public TimeConversionServiceTests(ITestOutputHelper output) : base(output)
    {
    }

    [Fact]
    public void Constructor_WithNoArguments_InstantiatesLogger()
    {
        Assert.NotNull(new TimeConversionService());
    }

    [Fact]
    public async Task ConvertTime_CurrentTimeWithEqualTimeZones_ReturnsInputTime()
    {
        var service = new TimeConversionService();
        var input = new TimeConversionInput
        {
            OriginTime = DateTime.Now,
            OriginTimeZone = TimeZoneInfo.Local,
            TargetTimeZone = TimeZoneInfo.Local
        };

        var result = await service.ConvertTimeAsync(input);
        var time = result.IfFail(error => throw error);

        Assert.Equal(DateTime.Now, time, Toleration);
        Assert.Equal(input.OriginTime, time, Toleration);
    }

    [Fact]
    public async Task ConvertTime_PastTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService();
        var originalTime = new DateTime(2001, 2, 21, 8, 0, 0);
        var expectedTime = new DateTime(2001, 2, 21, 14, 0, 0);

        var input = new TimeConversionInput
        {
            OriginTime = originalTime,
            OriginTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York"),
            TargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin")
        };

        var result = await service.ConvertTimeAsync(input);
        var convertedTime = result.IfFail(error => throw error);

        Assert.Equal(expectedTime, convertedTime, Toleration);
    }

    [Fact]
    public async Task ConvertTime_FutureTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService();

        var originTime = new DateTime(2026, 2, 21, 8, 0, 0);
        var originTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York");
        var expectedTime = new DateTime(2026, 2, 21, 14, 0, 0);
        var targetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin");

        var input = new TimeConversionInput
        {
            OriginTime = originTime,
            OriginTimeZone = originTimeZone,
            TargetTimeZone = targetTimeZone
        };

        var result = await service.ConvertTimeAsync(input);
        var convertedTime = result.IfFail(error => throw error);

        Assert.Equal(expectedTime, convertedTime, Toleration);
    }
}