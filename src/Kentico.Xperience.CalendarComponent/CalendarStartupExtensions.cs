using Kentico.Xperience.CalendarComponent.ValueProviders;

using Microsoft.Extensions.DependencyInjection;

namespace Kentico.Xperience.CalendarComponent;

/// <summary>
/// Application startup extension methods.
/// </summary>
public static class CalendarComponentStartipExtensions
{
    /// <summary>
    /// Registers instances of <see cref="ICalendarDataProvider"/>.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <param name="configure">The application configuration.</param>
    public static IServiceCollection AddKenticoCalendarComponent(this IServiceCollection serviceCollection, Action<ICalendarComponentBuilder> configure)
    {
        var builder = new CalendarComponentBuilder(serviceCollection);

        configure(builder);

        return serviceCollection;
    }
}

public interface ICalendarComponentBuilder
{
    /// <summary>
    /// Registers the given <typeparamref name="TDataProvider" /> as a transient service under <paramref name="providerName" />
    /// </summary>
    /// <typeparam name="TDataProvider">The custom type of <see cref="DefaultCalendarDataProvider"/> </typeparam>
    /// <param name="providerName">Used internally with <typeparamref name="TDataProvider" /> to enable dynamic assignment of calendar data providers. Names must be unique.</param>
    /// <exception cref="ArgumentException">
    ///     Thrown if a provider has already been registered with the given <paramref name="providerName"/>
    /// </exception>
    /// <returns></returns>
    ICalendarComponentBuilder RegisterDataProvider<TDataProvider>(string providerName) where TDataProvider : DefaultCalendarDataProvider;
}

internal class CalendarComponentBuilder : ICalendarComponentBuilder
{
    private readonly IServiceCollection serviceCollection;

    public CalendarComponentBuilder(IServiceCollection serviceCollection) => this.serviceCollection = serviceCollection;

    public ICalendarComponentBuilder RegisterDataProvider<TDataProvider>(string providerName) where TDataProvider : DefaultCalendarDataProvider
    {
        CalendarProviderStorage.AddCalendarDataProvider<TDataProvider>(providerName);
        serviceCollection.AddTransient<TDataProvider>();

        return this;
    }
}
