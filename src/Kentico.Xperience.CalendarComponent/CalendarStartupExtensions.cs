using Kentico.Xperience.CalendarComponent.ValueProviders;

using Microsoft.Extensions.DependencyInjection;

namespace Kentico.Xperience.CalendarComponent;

public static class CalendarComponentStartipExtensions
{
    public static IServiceCollection AddKenticoCalendarComponent(this IServiceCollection serviceCollection, Action<ICalendarComponentBuilder> configure)
    {
        var builder = new CalendarComponentBuilder(serviceCollection);

        configure(builder);

        return serviceCollection;
    }
}

public interface ICalendarComponentBuilder
{
    ICalendarComponentBuilder RegisterDataProvider<TDataProvider>(string providerName) where TDataProvider : class, ICalendarDataProvider;
}

internal class CalendarComponentBuilder : ICalendarComponentBuilder
{
    private readonly IServiceCollection serviceCollection;

    public CalendarComponentBuilder(IServiceCollection serviceCollection) => this.serviceCollection = serviceCollection;

    public ICalendarComponentBuilder RegisterDataProvider<TDataProvider>(string providerName) where TDataProvider : class, ICalendarDataProvider
    {
        CalendarProviderStorage.AddCalendarDataProvider<TDataProvider>(providerName);
        serviceCollection.AddTransient<TDataProvider>();

        return this;
    }
}
