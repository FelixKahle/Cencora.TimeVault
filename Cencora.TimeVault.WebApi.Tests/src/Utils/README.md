# Utils

## Overview

This utility library provides a custom implementation of ILogger<T> that writes log messages to the ITestOutputHelper
from xUnit. This is particularly useful for capturing and displaying logs from classes that utilize the Microsoft
logging framework during unit tests, helping with debugging and verification in your test scenarios.

## Key Features

- **Seamless Integration with xUnit**: The utility bridges the gap between the Microsoft logging framework and xUnit's
  ITestOutputHelper, enabling you to easily capture log output within your unit tests.
- **Flexible Logger Creation**: Create a logger instance for your tests by calling the BuildLoggerFor<TLogger>()
  extension method on ITestOutputHelper. This allows you to inject a logger into the classes you're testing, making it
  easy to verify log messages or simply observe logging behavior.
- **Convenient Base Class**: For a more streamlined experience, you can extend your test class from
  TestLoggerBase<TLogger>. This base class automatically provides access to an ILogger<TLogger> instance, reducing
  boilerplate code and making your test setup cleaner and more maintainable.

## Creating a Logger

To create a logger in your test class, simply call the `BuildLoggerFor<TLogger>()` method on an instance
of `ITestOutputHelper`. This logger can then be injected into the class under test.

```csharp
public class MyServiceTests
{
    private readonly ITestOutputHelper _output;

    public MyServiceTests(ITestOutputHelper output)
    {
        _output = output;
    }

    [Fact]
    public void TestMethod()
    {
        ILogger<MyService> logger = _output.BuildLoggerFor<MyService>();
        MyService service = new Service(logger);
    }
}
```

## Using the Base Class

Alternatively, you can simplify your tests by extending from `TestLoggerBase<TLogger>`. This approach provides a
pre-configured `ILogger<TLogger>` that you can use directly within your test methods.

```csharp
public class MyServiceTests : TestLoggerBase<MyService>
{
    public MyServiceTests(ITestOutputHelper output)
        : base(output)
    {}

    [Fact]
    public void TestMethod()
    {
        MyService service = new MyService(Logger)
    }
}
```

## Advantages

- **Enhanced Debugging**: By capturing log output directly in your test results, you can better understand how your code
  behaves during execution and catch issues early.
- **Clean and Reusable Code**: Whether you use the BuildLoggerFor method or the TestLoggerBase class, your test code
  will be cleaner, more organized, and easier to maintain.
- **Flexible Integration**: The provided utilities are non-intrusive and can be easily integrated into existing tests,
  requiring minimal changes to your current setup.

## Notes

We are considering moving this functionality into a separate project and publishing it as a NuGet package in the future.
This would make it easier for other developers to integrate this logging utility into their own projects and benefit
from the same streamlined testing experience.

## Conclusion

This utility makes it easier to incorporate logging into your unit tests, ensuring that you have full visibility into
the behavior of your code during test execution. By leveraging ILogger<T> and ITestOutputHelper together, you can
produce more informative and reliable test outputs, ultimately leading to higher-quality code.