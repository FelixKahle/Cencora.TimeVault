// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Services.TimeConversion;
using Cencora.TimeVault.WebApi.Tests.Utils;
using Xunit.Abstractions;

namespace Cencora.TimeVault.WebApi.Tests.TimeConversion.Services;

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
        var logger = Logger;
        Assert.NotNull(new TimeConversionService(logger));
    }

    [Fact]
    public void Constructor_WithNullLogger_ThrowsArgumentNullException()
    {
        Assert.Throws<ArgumentNullException>(() => new TimeConversionService(null!));
    }

    [Fact]
    public void ConvertTime_CurrentTimeWithEqualTimeZones_ReturnsInputTime()
    {
        var service = new TimeConversionService(Logger);
        var input = new TimeConversionInput
        {
            OriginTime = DateTime.Now,
            OriginTimeZone = TimeZoneInfo.Local,
            TargetTimeZone = TimeZoneInfo.Local
        };

        var result = service.ConvertTime(input);

        Assert.Equal(DateTime.Now, result.ConvertedTime, Toleration);
        Assert.Equal(input.OriginTime, result.ConvertedTime, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public void ConvertTime_CurrentTimeWithDifferentTimeZones_ReturnsConvertedTime()
    {
        var service = new TimeConversionService(Logger);
        var input = new TimeConversionInput
        {
            OriginTime = DateTime.Now,
            OriginTimeZone = TimeZoneInfo.Local,
            TargetTimeZone = TimeZoneInfo.Utc
        };

        var result = service.ConvertTime(input);

        Assert.Equal(DateTime.Now, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public void ConvertTime_PastTimeWithDifferentTimeZones_ReturnsConvertedTime()
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

        var result = service.ConvertTime(input);

        Assert.Equal(originalTime, result.OriginTime, Toleration);
        Assert.Equal(expectedTime, result.ConvertedTime, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }

    [Fact]
    public void ConvertTime_FutureTimeWithDifferentTimeZones_ReturnsConvertedTime()
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

        var result = service.ConvertTime(input);

        Assert.Equal(originTime, result.OriginTime, Toleration);
        Assert.Equal(expectedTime, result.ConvertedTime, Toleration);
        Assert.Equal(input.OriginTime, result.OriginTime, Toleration);
        Assert.Equal(input.OriginTimeZone, result.OriginTimeZone);
        Assert.Equal(input.TargetTimeZone, result.TargetTimeZone);
    }
}