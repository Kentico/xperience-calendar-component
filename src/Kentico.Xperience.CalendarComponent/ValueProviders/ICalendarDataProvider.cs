namespace Kentico.Xperience.CalendarComponent.ValueProviders;

public interface ICalendarDataProvider
{
    Task<IEnumerable<DateOnly>> GetUnavailableDates();
    Task<IEnumerable<DateTime>> GetUnavailableTimeFrames();
}

public abstract class DefaultCalendarDataProvider : ICalendarDataProvider
{
    public virtual async Task<IEnumerable<DateOnly>> GetUnavailableDates() => await Task.FromResult(new List<DateOnly>());
    public virtual async Task<IEnumerable<DateTime>> GetUnavailableTimeFrames() => await Task.FromResult(new List<DateTime>());
}
