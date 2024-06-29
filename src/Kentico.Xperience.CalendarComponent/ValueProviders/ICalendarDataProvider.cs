namespace Kentico.Xperience.CalendarComponent.ValueProviders;

public interface ICalendarDataProvider
{
    Task<IEnumerable<DateTime>> GetUnavailableValues();
}

public abstract class DefaultCalendarDataSource : ICalendarDataProvider
{
    public virtual async Task<IEnumerable<DateTime>> GetUnavailableValues() => await Task.FromResult(new List<DateTime>());
}
