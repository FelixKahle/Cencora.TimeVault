// Copyright © 2024 Cencora. All rights reserved.
//
// Written by Felix Kahle, A123234, felix.kahle@worldcourier.de

using Cencora.TimeVault.WebApi.Models;

namespace Cencora.TimeVault.WebApi.Services.TimeZone;

/// <summary>
/// An exception that is thrown when a location was not found.
/// </summary>
public class LocationNotFoundException : Exception
{
    /// <summary>
    /// The location that was not found.
    /// </summary>
    public Location Location { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationNotFoundException"/> class.
    /// </summary>
    /// <param name="location">The location that was not found.</param>
    /// <param name="message">The message that describes the error.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    public LocationNotFoundException(Location location, string message, Exception innerException) 
        : base(message, innerException)
    {
        Location = location;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationNotFoundException"/> class.
    /// </summary>
    /// <param name="location">The location that was not found.</param>
    /// <param name="message">The message that describes the error.</param>
    public LocationNotFoundException(Location location, string message) 
        : base(message)
    {
        Location = location;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationNotFoundException"/> class.
    /// </summary>
    /// <param name="location">The location that was not found.</param>
    /// <remarks>
    /// The message that describes the error is automatically generated.
    /// </remarks>
    public LocationNotFoundException(Location location) 
        : base($"The location {location} was not found.")
    {
        Location = location;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LocationNotFoundException"/> class.
    /// </summary>
    /// <param name="location">The location that was not found.</param>
    /// <param name="innerException">The exception that is the cause of the current exception.</param>
    /// <remarks>
    /// The message that describes the error is automatically generated.
    /// </remarks>
    public LocationNotFoundException(Location location, Exception innerException) 
        : base($"The location {location} was not found.", innerException)
    {
        Location = location;
    }
}