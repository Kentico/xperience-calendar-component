namespace Kentico.Xperience.CalendarComponent.ValueProviders;

/// <summary>
/// Dynamic calendar data provider. Provider can be selected in Kentico Form builder in the administration.
/// </summary>
public interface ICalendarDataProvider
{
    /// <summary>
    /// Disable dates for a calendar.
    /// </summary>
    /// <returns><see cref="IEnumerable{DateOnly}"/> with disabled date values</returns>
    Task<IEnumerable<DateOnly>> GetUnavailableDates();

    /// <summary>
    /// Disable DateTime values for a calendar.
    /// </summary>
    /// <returns><see cref="IEnumerable{DateTime}"/> with disabled date time values</returns>
    Task<IEnumerable<DateTime>> GetUnavailableTimeFrames();

    /// <summary>
    /// Set minimal time selectable in any day.
    /// </summary>
    /// <returns><see cref="TimeSpan"/> representing minimal selectable time in any day</returns>
    TimeSpan GetMinTime();

    /// <summary>
    /// Set maximal time selectable in any day.
    /// </summary>
    /// <returns><see cref="TimeSpan"/> representing maximual selectable time in any day</returns>
    TimeSpan GetMaxTime();

    /// <summary>
    /// Set minimal selectable Date.
    /// </summary>
    /// <returns><see cref="DateOnly"/> representing minimal selectable date</returns>
    DateOnly GetMinDate();

    /// <summary>
    /// Set maximal selectable Date.
    /// </summary>
    /// <returns><see cref="DateOnly"/> representing maximal selectable date</returns>
    DateOnly GetMaxDate();
}

/// <summary>
/// Default data provider just implements the methods but does not change the data.
/// </summary>
public abstract class AbstractCalendarDataProvider : ICalendarDataProvider
{
    /// <inheritdoc />
    public virtual async Task<IEnumerable<DateOnly>> GetUnavailableDates() => await Task.FromResult(new List<DateOnly>());

    /// <inheritdoc />
    public virtual async Task<IEnumerable<DateTime>> GetUnavailableTimeFrames() => await Task.FromResult(new List<DateTime>());

    /// <inheritdoc />
    public virtual TimeSpan GetMinTime() => new(0, 0, 0);

    /// <inheritdoc />
    public virtual TimeSpan GetMaxTime() => new(23, 59, 59);

    /// <inheritdoc />
    public virtual DateOnly GetMinDate() => DateOnly.MinValue;

    /// <inheritdoc />
    public virtual DateOnly GetMaxDate() => DateOnly.MaxValue;
}
