using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace DancingGoat.CalendarData
{
    public class DancingGoatCalendarDataProvider : ICalendarDataProvider
    {
        public async Task<IEnumerable<DateTime>> GetUnavailableValues() => await Task.FromResult(
            new List<DateTime> {
                DateTime.Now,
                DateTime.Now.AddDays(1),
                DateTime.Now.AddDays(-1),
                DateTime.Now.AddDays(5),
                DateTime.Now.AddDays(10)
            });
    }
}
