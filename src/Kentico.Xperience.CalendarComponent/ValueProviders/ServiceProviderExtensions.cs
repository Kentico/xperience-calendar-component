using Microsoft.Extensions.DependencyInjection;

namespace Kentico.Xperience.CalendarComponent.ValueProviders;

public static class ServiceProviderExtensions
{
    internal static ICalendarDataProvider GetRequiredCalendarDataProvider(this IServiceProvider serviceProvider, Type dataProviderType)
    {
        var dataProvider = serviceProvider.GetRequiredService(dataProviderType) as ICalendarDataProvider;

        return dataProvider!;
    }
}
