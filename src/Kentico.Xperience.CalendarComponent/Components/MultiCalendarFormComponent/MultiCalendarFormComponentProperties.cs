using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class MultiCalendarFormComponentProperties : FormComponentProperties<string>
{
    public const string CUSTOM_FORMAT_IDENTIFIER = "Custom";
    public const string NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER = "None";

    public MultiCalendarFormComponentProperties() : base(FieldDataType.Text, size: 200)
    {
    }

    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Is multi date selection",
        DefaultValue = false,
        ExplanationText = "Check for multi value selection. Uncheck for Range selection.",
        Order = 1)]
    public bool IsMulti { get; set; }


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Date Format",
        DefaultValue = "m.d.Y",
        ExplanationText = "Select Date format",
        Order = 5)]
    [EditingComponentConfiguration(typeof(DateTimeRangeFormatConfigurator))]
    public string DateFormat { get; set; } = string.Empty;


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded dates. Choose \"None\" for no provider.")]
    [EditingComponentConfiguration(typeof(ExcludedRangeDateTimeProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;


    [DefaultValueEditingComponent(MultiCalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(DefaultRangeValueConfigurator), nameof(DateFormat))]
    public override string DefaultValue { get; set; } = string.Empty;
}

public class DateTimeRangeFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonDateTimeFormats = new() {
        "d.m.Y", "m.d.Y", "m/d/Y", "d/m/Y", "d.m.y", "m.d.y", "m/d/y", "d/m/y"
        ,"y.m.d", "y.d.m", "y/m/d", "y/d/m", "Y.m.d", "Y.d.m", "Y/m/d", "Y/d/m",
        "d-m-Y", "d-m-y", "m-d-Y", "m-d-y", "y-m-d", "Y-m-d", "y-d-m", "Y-d-m"
    };

    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider) =>
        formComponent.Properties.DataSource = $"{string.Join("\r\n", commonDateTimeFormats)}\r\n{MultiCalendarFormComponentProperties.CUSTOM_FORMAT_IDENTIFIER}";
}

public class DefaultRangeValueConfigurator : FormComponentConfigurator<MultiCalendarFormComponent>
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

internal class ExcludedRangeDateTimeProviderConfigurator : FormComponentConfigurator<DropDownComponent>
{
    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", CalendarProviderStorage.Providers.Keys)}\r\n{MultiCalendarFormComponentProperties.NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER}";
}
