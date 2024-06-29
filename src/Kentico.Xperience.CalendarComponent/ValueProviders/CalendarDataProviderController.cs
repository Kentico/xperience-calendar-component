using Microsoft.AspNetCore.Mvc;

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
            return Json(new List<DateTime>());
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

        return Json(await dataProvider.GetUnavailableValues());
    }
}
