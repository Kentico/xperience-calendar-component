using Microsoft.Extensions.DependencyInjection;

namespace Kentico.Xperience.CalendarComponent.ValueProviders;

internal static class ServiceProviderExtensions
{
    /// <summary>
    /// Returns an instance of the registered <see cref="ICalendarDataProvider"/>.
    /// Used to generate instances of a <see cref="ICalendarDataProvider"/> service type that can change at runtime.
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="dataProviderType"></param>
    /// <exception cref="InvalidOperationException">
    ///     Thrown if the assigned <see cref="ICalendarDataProvider"/> cannot be instantiated.
    ///     This shouldn't normally occur because we fallback to <see cref="AbstractCalendarDataProvider" /> if not custom provider is specified.
    ///     However, incorrect dependency management in user-code could cause issues.
    /// </exception>
    /// <returns></returns>
    public static ICalendarDataProvider GetRequiredCalendarDataProvider(this IServiceProvider serviceProvider, Type dataProviderType)
    {
        var dataProvider = serviceProvider.GetRequiredService(dataProviderType) as AbstractCalendarDataProvider;

        return dataProvider!;
    }
}
