# Create a dynamic Calendar data provider

You can programatically create a data provider which can disable individual dates and date times. These values will not be selectable in the form.

To create the provider:

Implement the `AbstractCalendarDataProvider` and override it's methods which you wish to configure.

Create `ExampleCalendarDataProvider`

```csharp
public class ExampleCalendarDataProvider : AbstractCalendarDataProvider
{
    // Override to exclude individual dates.
    public override async Task<IEnumerable<DateOnly>> GetUnavailableDates() => await Task.FromResult(
        new List<DateOnly> {
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(5))
        });

    // Override to exclude individual date-time values.
    public override async Task<IEnumerable<DateTime>> GetUnavailableTimeFrames() => await Task.FromResult(
        new List<DateTime>
        {
            DateTime.Now,
            DateTime.Now.AddMinutes(60),
            DateTime.Now.AddMinutes(120),
            DateTime.Now.AddMinutes(-45),
            DateTime.Now.AddMinutes(-121)
        });

    // Override to limit minimal selectable date.
    public override DateOnly GetMinDate() => DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));

    // Override to limit maximal selectable date.
    public override DateOnly GetMaxDate() => DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

    // Override to limit minimal selectable time in all days.
    public override TimeSpan GetMinTime() => new(hours: 9, minutes: 0, seconds: 0);

    // Override to limit maximal selectable time in all days.
    public override TimeSpan GetMaxTime() => new(hours: 17, minutes: 0, seconds: 0);
}
```

Register this provider in your `Startup.cs` like this:
```csharp
    // ... Other service registrations
    
    // ... After call to services.AddDancingGoatServices();

    services.AddKenticoCalendarComponent(builder =>
        builder.RegisterDataProvider<ExampleCalendarDataProvider>("Example calendar data provider")
    );
```

This registers your Provider to the Dependancy injection. You can use other application services or call a 3rd party endpoint to configure the data.

You must specify a unique name as the parameter of the `RegisterDataProvider` method. This name will be displayed in the administration.