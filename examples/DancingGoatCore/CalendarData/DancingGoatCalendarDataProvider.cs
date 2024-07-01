using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace DancingGoat.CalendarData
{
    public class DancingGoatCalendarDataProvider : DefaultCalendarDataProvider
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
    }
}
