namespace Kentico.Xperience.CalendarComponent.ValueProviders;

internal static class CalendarProviderStorage
{
    public static Dictionary<string, Type> Providers { get; private set; }
    static CalendarProviderStorage() => Providers = new Dictionary<string, Type>();

    public static void AddCalendarDataProvider<TProvider>(string providerName) where TProvider : ICalendarDataProvider
        => Providers.Add(providerName, typeof(TProvider));
}
