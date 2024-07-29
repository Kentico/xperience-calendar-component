using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace DancingGoat.CalendarData;

public class DancingGoatCalendarDataProvider : AbstractCalendarDataProvider
{
    public override async Task<IEnumerable<DateOnly>> GetUnavailableDates() => await Task.FromResult(
        new List<DateOnly> {
            DateOnly.FromDateTime(DateTime.Now.AddDays(1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(-1)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(3)),
            DateOnly.FromDateTime(DateTime.Now.AddDays(5))
        });

    public override async Task<IEnumerable<DateTime>> GetUnavailableTimeFrames() => await Task.FromResult(
        new List<DateTime>
        {
            DateTime.Now,
            DateTime.Now.AddMinutes(60),
            DateTime.Now.AddMinutes(120),
            DateTime.Now.AddMinutes(-45),
            DateTime.Now.AddMinutes(-121)
        });

    public override DateOnly GetMinDate() => DateOnly.FromDateTime(DateTime.Now.AddMonths(-1));

    public override DateOnly GetMaxDate() => DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

    public override TimeSpan GetMinTime() => new(hours: 9, minutes: 0, seconds: 0);

    public override TimeSpan GetMaxTime() => new(hours: 17, minutes: 0, seconds: 0);
}
