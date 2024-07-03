using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components;

internal class CalendarDefaultValueConfigurator : FormComponentConfigurator<CalendarFormComponent>
{
    public override void Configure(CalendarFormComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
    {
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.DateOnly), out bool dateOnly);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.DateFormat), out string dateTimeFormat);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.TimeInterval), out int timeInterval);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.ExcludedDateTimeDataProvider), out string dateTimeDataProvider);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.Is24HourFormat), out bool is24HourFormat);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.DefaultValue), out DateTime defaultValue);

        formComponent.Properties.DateOnly = dateOnly;
        formComponent.Properties.DefaultValue = defaultValue;
        formComponent.Properties.Is24HourFormat = is24HourFormat;
        formComponent.Properties.DateFormat = dateTimeFormat;
        formComponent.Properties.TimeInterval = timeInterval;
        formComponent.Properties.ExcludedDateTimeDataProvider = dateTimeDataProvider;
    }
}
