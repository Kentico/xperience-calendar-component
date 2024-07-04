using Kentico.Xperience.CalendarComponent.Components;

using Microsoft.AspNetCore.Mvc;

namespace Kentico.Xperience.CalendarComponent.ValueProviders;

[ApiController]
[Route("/kentico.calendarComponent/[action]")]
public sealed class CalendarDataProviderController : Controller
{
    private readonly IServiceProvider serviceProvider;
    public CalendarDataProviderController(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

    /// <summary>
    /// Returns data from dynamic data provider for a given calendar form component.
    /// </summary>
    /// <param name="dataProviderName">name of the provider</param>
    /// <returns></returns>
    /// <exception cref="InvalidDataException"></exception>
    [HttpGet]
    public async Task<IActionResult> GetExcludedDateTimeData(string dataProviderName)
    {
        if (dataProviderName is null or CalendarFormComponentProperties.NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER)
        {
            return Json(new CalendarDataDto());
        }

        var dataProviderType = CalendarProviderStorage.Providers[dataProviderName];

        if (dataProviderType == null)
        {
            throw new InvalidDataException($"Specified provider \"{dataProviderType}\" does not exist.");
        }

        var dataProvider = serviceProvider.GetRequiredCalendarDataProvider(dataProviderType) ?? throw new InvalidDataException($"Specified provider \"{dataProviderType}\" does not exist.");

        var excludedDates = (await dataProvider.GetUnavailableDates()).Select(x => x.ToString()).ToList();
        var excludedTimeFrames = (await dataProvider.GetUnavailableTimeFrames()).Select(x => x.ToString()).ToList();

        var result = new CalendarDataDto(excludedTimeFrames, excludedDates)
        {
            MinTime = dataProvider.GetMinTime().ToString("hh\\:mm"),
            MaxTime = dataProvider.GetMaxTime().ToString("hh\\:mm"),
            MinDate = dataProvider.GetMinDate().ToString(),
            MaxDate = dataProvider.GetMaxDate().ToString()
        };

        return Json(result);
    }
}

internal class CalendarDataDto
{
    public List<string> ExcludedTimeFrames { get; set; }
    public List<string> ExcludedDates { get; set; }
    public string MinTime { get; set; } = string.Empty;
    public string MaxTime { get; set; } = string.Empty;
    public string MinDate { get; set; } = string.Empty;
    public string MaxDate { get; set; } = string.Empty;

    public CalendarDataDto()
    {
        ExcludedTimeFrames = new List<string>();
        ExcludedDates = new List<string>();
    }

    public CalendarDataDto(List<string> excludedTimeFrames, List<string> excludedDates)
    {
        ExcludedTimeFrames = excludedTimeFrames;
        ExcludedDates = excludedDates;
    }
}
