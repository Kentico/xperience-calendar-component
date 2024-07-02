using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class CalendarFormComponentProperties : FormComponentProperties<DateTime>
{
    public const string CUSTOM_FORMAT_IDENTIFIER = "Custom";
    public const string NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER = "None";

    public CalendarFormComponentProperties() : base(FieldDataType.DateTime)
    {
    }

    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Show Date Only",
        DefaultValue = false,
        ExplanationText = "Check for date-only selection. Displays date and time when unchecked",
        Order = 2)]
    public bool DateOnly { get; set; }


    [EditingComponent(IntInputComponent.IDENTIFIER,
        Label = "Time Frame",
        DefaultValue = 15,
        ExplanationText = "Select number of minutes in each individual time frame",
        Order = 3)]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public int TimeInterval { get; set; }


    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Display in 24-Hour format",
        DefaultValue = false,
        ExplanationText = "Check for 24-Hour format, uses 12-Hour format by default.",
        Order = 4)]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public bool Is24HourFormat { get; set; }


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Date Format",
        DefaultValue = "M.d.Y",
        ExplanationText = "Select date format",
        Order = 5)]
    [EditingComponentConfiguration(typeof(DateTimeFormatConfigurator), nameof(DateOnly))]
    public string DateFormat { get; set; } = string.Empty;


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded date and time frames. Choose \"None\" option for no provider.")]
    [EditingComponentConfiguration(typeof(ExcludedDateTimeProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;


    [DefaultValueEditingComponent(CalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(DefaultValueConfigurator), nameof(DateFormat))]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;
}

public class DateTimeFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonDateTimeFormats = new() {
        "d.M.Y", "M.d.Y", "M/d/Y", "d/M/Y", "d.M.y", "M.d.y", "M/d/y", "d/M/y"
        ,"y.M.d", "y.d.M", "y/M/d", "y/d/M", "Y.M.d", "Y.d.M", "Y/M/d", "Y/d/M",
        "d-M-Y", "d-M-y", "M-d-Y", "M-d-y", "y-M-d", "Y-M-d", "y-d-M", "Y-d-M"
    };

    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", commonDateTimeFormats)}\r\n{CalendarFormComponentProperties.CUSTOM_FORMAT_IDENTIFIER}";
}

public class DefaultValueConfigurator : FormComponentConfigurator<CalendarFormComponent>
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

internal class ExcludedDateTimeProviderConfigurator : FormComponentConfigurator<DropDownComponent>
{
    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", CalendarProviderStorage.Providers.Keys)}\r\n{CalendarFormComponentProperties.NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER}";
}
