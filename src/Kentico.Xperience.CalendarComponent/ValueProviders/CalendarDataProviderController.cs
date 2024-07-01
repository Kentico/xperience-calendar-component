﻿using Microsoft.AspNetCore.Mvc;

using Kentico.Xperience.CalendarComponent.Components.FormComponents;

namespace Kentico.Xperience.CalendarComponent.ValueProviders;

[ApiController]
[Route("/kentico.calendarComponent/[action]")]
public sealed class CalendarDataProviderController : Controller
{
    private readonly IServiceProvider serviceProvider;
    public CalendarDataProviderController(IServiceProvider serviceProvider) => this.serviceProvider = serviceProvider;

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

        var dataProvider = serviceProvider.GetRequiredCalendarDataProvider(dataProviderType);

        if (dataProvider is null)
        {
            throw new InvalidDataException($"Specified provider \"{dataProviderType}\" does not exist.");
        }

        var excludedDates = (await dataProvider.GetUnavailableDates()).Select(x => x.ToString()).ToList();
        var excludedTimeFrames = (await dataProvider.GetUnavailableTimeFrames()).Select(x => x.ToString()).ToList();

        var result = new CalendarDataDto(excludedTimeFrames, excludedDates);

        return Json(result);
    }
}

public class CalendarDataDto
{
    public List<string> ExcludedTimeFrames { get; set; }
    public List<string> ExcludedDates { get; set; }

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
