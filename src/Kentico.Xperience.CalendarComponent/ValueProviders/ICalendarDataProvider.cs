namespace Kentico.Xperience.CalendarComponent.ValueProviders;

public interface ICalendarDataProvider
{
    Task<IEnumerable<DateOnly>> GetUnavailableDates();
    Task<IEnumerable<DateTime>> GetUnavailableTimeFrames();
    TimeSpan GetMinTime();
    TimeSpan GetMaxTime();
    DateOnly GetMinDate();
    DateOnly GetMaxDate();
}

public abstract class DefaultCalendarDataProvider : ICalendarDataProvider
{
    public virtual async Task<IEnumerable<DateOnly>> GetUnavailableDates() => await Task.FromResult(new List<DateOnly>());
    public virtual async Task<IEnumerable<DateTime>> GetUnavailableTimeFrames() => await Task.FromResult(new List<DateTime>());
    public virtual TimeSpan GetMinTime() => new(0, 0, 0);
    public virtual TimeSpan GetMaxTime() => new(23, 59, 59);
    public virtual DateOnly GetMinDate() => DateOnly.MinValue;
    public virtual DateOnly GetMaxDate() => DateOnly.MaxValue;
}
