# Time Conversion

This folder contains a time conversion service. The service provides functionality to convert time between different time zones, ensuring that time-sensitive operations are handled consistently across different regions.

## Usage

### Add the Service to the DI Container:

```csharp
services.AddTimeConversionService();
```
This will register a default implementation with the DI container as a singleton.

### Invoke Time Conversion:

Use the service in your application by injecting `ITimeConversionService` and calling the `ConvertTime` method with appropriate input:

```csharp
var result = _timeConversionService.ConvertTime(new TimeConversionInput
{
    OriginTime = DateTime.UtcNow,
    OriginTimeZone = originTimeZone
    TargetTimeZone = targetTimeZone
});

Console.WriteLine($"Converted Time: {result.ConvertedTime}");
```

## Custom Time Conversion Service Implementation

If the default time conversion logic doesn't meet your specific requirements, you can implement your own service by following these steps:

### Create a Custom Service:

Implement a new class that adheres to the `ITimeConversionService` interface, applying your specific conversion logic:

```csharp
public class CustomTimeConversionService : ITimeConversionService
{
    public TimeConversionResult ConvertTime(TimeConversionInput input)
    {
        // Your logic
        return yourResult;
    }
}
```

### Register the Custom Service:

Replace the default TimeConversionService with your custom implementation in the DI container, typically done in **Startup.cs** or **Program.cs**:

```csharp
services.AddTransient<ITimeConversionService, CustomTimeConversionService>();
```
Or use one the provided shortcut, this will register your implementation as a singleton in the DI container:

```csharp
services.AddTimeConversion<CustomTimeConversionService>();
```

Inject and use ITimeConversionService as usual; the application will now utilize your custom logic.