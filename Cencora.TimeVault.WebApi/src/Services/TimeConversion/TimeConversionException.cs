// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

namespace Cencora.TimeVault.WebApi.Services.TimeConversion;

/// <summary>
/// Represents an exception that occurs during time conversion.
/// </summary>
public class TimeConversionException : Exception
{
    /// <summary>
    /// Gets the origin time that was being converted.
    /// </summary>
    public DateTime OriginTime { get; }

    /// <summary>
    /// Gets the origin time zone of the conversion.
    /// </summary>
    public TimeZoneInfo OriginTimeZone { get; }

    /// <summary>
    /// Gets the target time zone of the conversion.
    /// </summary>
    public TimeZoneInfo TargetTimeZone { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionException"/> class with the specified origin time, origin time zone, target time zone, message, and inner exception.
    /// </summary>
    /// <param name="originTime">The origin time that was being converted.</param>
    /// <param name="originTimeZone">The origin time zone of the conversion.</param>
    /// <param name="targetTimeZone">The target time zone of the conversion.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public TimeConversionException(DateTime originTime, TimeZoneInfo originTimeZone, TimeZoneInfo targetTimeZone, string message, Exception innerException) 
        : base(message, innerException)
    {
        OriginTime = originTime;
        OriginTimeZone = originTimeZone;
        TargetTimeZone = targetTimeZone;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionException"/> class with the specified origin time, origin time zone, target time zone, and message.
    /// </summary>
    /// <param name="originTime">The origin time that was being converted.</param>
    /// <param name="originTimeZone">The origin time zone of the conversion.</param>
    /// <param name="targetTimeZone">The target time zone of the conversion.</param>
    /// <param name="message">The error message that explains the reason for the exception.</param>
    public TimeConversionException(DateTime originTime, TimeZoneInfo originTimeZone, TimeZoneInfo targetTimeZone, string message) 
        : base(message)
    {
        OriginTime = originTime;
        OriginTimeZone = originTimeZone;
        TargetTimeZone = targetTimeZone;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionException"/> class with the specified origin time, origin time zone, and target time zone.
    /// </summary>
    /// <param name="originTime">The origin time that was being converted.</param>
    /// <param name="originTimeZone">The origin time zone of the conversion.</param>
    /// <param name="targetTimeZone">The target time zone of the conversion.</param>
    public TimeConversionException(DateTime originTime, TimeZoneInfo originTimeZone, TimeZoneInfo targetTimeZone) 
        : base($"An error occurred while converting the time {originTime} from {originTimeZone} to {targetTimeZone}.")
    {
        OriginTime = originTime;
        OriginTimeZone = originTimeZone;
        TargetTimeZone = targetTimeZone;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TimeConversionException"/> class with the specified origin time, origin time zone, target time zone, and inner exception.
    /// </summary>
    /// <param name="originTime">The origin time that was being converted.</param>
    /// <param name="originTimeZone">The origin time zone of the conversion.</param>
    /// <param name="targetTimeZone">The target time zone of the conversion.</param>
    /// <param name="innerException">The exception that is the cause of the current exception, or a null reference if no inner exception is specified.</param>
    public TimeConversionException(DateTime originTime, TimeZoneInfo originTimeZone, TimeZoneInfo targetTimeZone, Exception innerException) 
        : base($"An error occurred while converting the time {originTime} from {originTimeZone} to {targetTimeZone}.", innerException)
    {
        OriginTime = originTime;
        OriginTimeZone = originTimeZone;
        TargetTimeZone = targetTimeZone;
    }
}