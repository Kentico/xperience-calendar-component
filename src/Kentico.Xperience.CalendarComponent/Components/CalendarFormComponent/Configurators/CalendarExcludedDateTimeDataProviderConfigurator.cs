using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace Kentico.Xperience.CalendarComponent.Components;

internal class CalendarExcludedDateTimeDataProviderConfigurator : FormComponentConfigurator<DropDownComponent>
{
    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", CalendarProviderStorage.Providers.Keys)}\r\n{CalendarFormComponentProperties.NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER}";
}
