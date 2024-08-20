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
    public void Constructor_WithValidArguments_InitializesLogger()
    {
        Assert.NotNull(new TimeConversionService(Logger));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TimeConversionService(null!));
    }

    [Fact]
    public async Task ConvertTime_CurrentTimeWithEqualTimeZones_ReturnsInputTime()
    {
        var service = new TimeConversionService(Logger);
        var input = new TimeConversionInput
        {
            OriginTime = DateTime.Now,
            OriginTimeZone = TimeZoneInfo.Local,
            TargetTimeZone = TimeZoneInfo.Local
        };

        var result = await service.ConvertTimeAsync(input);

        Assert.Equal(DateTime.Now, result.ConvertedTime!.Value, Toleration);
        Assert.Equal(input.OriginTime, result.ConvertedTime!.Value, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public async Task ConvertTime_CurrentTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService(Logger);
        var input = new TimeConversionInput
        {
            OriginTime = DateTime.Now,
            OriginTimeZone = TimeZoneInfo.Local,
            TargetTimeZone = TimeZoneInfo.Utc
        };

        var result = await service.ConvertTimeAsync(input);

        Assert.Equal(DateTime.Now, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public async Task ConvertTime_PastTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService(Logger);
        var originalTime = new DateTime(2001, 2, 21, 8, 0, 0);
        var expectedTime = new DateTime(2001, 2, 21, 14, 0, 0);

        var input = new TimeConversionInput
        {
            OriginTime = originalTime,
            OriginTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/New_York"),
            TargetTimeZone = TimeZoneInfo.FindSystemTimeZoneById("Europe/Berlin")
        };

        var result = await service.ConvertTimeAsync(input);

        Assert.Equal(originalTime, result.OriginTime, Toleration);
        Assert.Equal(expectedTime, result.ConvertedTime!.Value, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public async Task ConvertTime_FutureTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService(Logger);

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

        Assert.Equal(originTime, result.OriginTime, Toleration);
        Assert.Equal(expectedTime, result.ConvertedTime!.Value, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }
}