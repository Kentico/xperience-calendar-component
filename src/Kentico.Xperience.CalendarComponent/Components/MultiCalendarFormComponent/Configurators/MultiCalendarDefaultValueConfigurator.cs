using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components;

internal class MultiCalendarDefaultValueConfigurator : FormComponentConfigurator<MultiCalendarFormComponent>
{
    public override void Configure(MultiCalendarFormComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
    {
        formFieldValueProvider.TryGet(nameof(MultiCalendarFormComponentProperties.DateFormat), out string dateTimeFormat);
        formFieldValueProvider.TryGet(nameof(MultiCalendarFormComponentProperties.ExcludedDateTimeDataProvider), out string dateTimeDataProvider);
        formFieldValueProvider.TryGet(nameof(MultiCalendarFormComponentProperties.IsMulti), out bool isMulti);
        formFieldValueProvider.TryGet(nameof(MultiCalendarFormComponentProperties.DefaultValue), out string defaultValue);

        formComponent.Properties.DateFormat = dateTimeFormat;
        formComponent.Properties.DefaultValue = defaultValue;
        formComponent.Properties.IsMulti = isMulti;
        formComponent.Properties.ExcludedDateTimeDataProvider = dateTimeDataProvider;
    }
}
