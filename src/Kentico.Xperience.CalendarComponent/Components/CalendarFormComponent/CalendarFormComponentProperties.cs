using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.ValueProviders;

namespace Kentico.Xperience.CalendarComponent.Components;

/// <summary>
/// The properties to be set in the Kentico administration to configure the calendar form component.
/// </summary>
public class CalendarFormComponentProperties : FormComponentProperties<DateTime>
{
    /// <summary>
    /// Identifier of the custom date format.
    /// </summary>
    internal const string CUSTOM_FORMAT_IDENTIFIER = "Custom";

    /// <summary>
    /// Identifier of no dynamic data provider.
    /// </summary>
    internal const string NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER = "None";

    /// <summary>
    /// Initializes a new instance of Kentico.Xperience.CalendarComponent.Components.CalendarFormComponentProperties
    /// </summary>
    public CalendarFormComponentProperties() : base(FieldDataType.DateTime)
    {
    }

    /// <summary>
    /// Configures the calendar component to use date or date and time format.
    /// </summary>
    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Show Date Only",
        DefaultValue = false,
        ExplanationText = "Check for date-only selection. Displays date and time when unchecked",
        Order = 2)]
    public bool DateOnly { get; set; }

    /// <summary>
    /// Configures the length of a time frame.
    /// </summary>
    [EditingComponent(IntInputComponent.IDENTIFIER,
        Label = "Time Frame",
        DefaultValue = 15,
        ExplanationText = "Select number of minutes in each individual time frame",
        Order = 3)]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public int TimeInterval { get; set; }

    /// <summary>
    /// Configures whether the time is displayed in 24-hour or 12-hour AM/PM day format.
    /// </summary>
    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Display in 24-Hour format",
        DefaultValue = false,
        ExplanationText = "Check for 24-Hour format, uses 12-Hour format by default.",
        Order = 4)]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public bool Is24HourFormat { get; set; }

    /// <summary>
    /// Configures displayed date format.
    /// </summary>
    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Date Format",
        DefaultValue = "M.d.Y",
        ExplanationText = "Select date format",
        Order = 5)]
    [EditingComponentConfiguration(typeof(DateTimeFormatConfigurator), nameof(DateOnly))]
    public string DateFormat { get; set; } = string.Empty;

    /// <summary>
    /// Configures the dynamic calendar data provider.
    /// </summary>
    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded date and time frames. Choose \"None\" option for no provider.")]
    [EditingComponentConfiguration(typeof(ExcludedDateTimeProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;

    /// <summary>
    /// Sets the default value for calendar component.
    /// </summary>
    [DefaultValueEditingComponent(CalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(DefaultValueConfigurator), nameof(DateFormat))]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;
}

internal class DateTimeFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonDateTimeFormats = new() {
        "d.M.Y", "M.d.Y", "M/d/Y", "d/M/Y", "d.M.y", "M.d.y", "M/d/y", "d/M/y"
        ,"y.M.d", "y.d.M", "y/M/d", "y/d/M", "Y.M.d", "Y.d.M", "Y/M/d", "Y/d/M",
        "d-M-Y", "d-M-y", "M-d-Y", "M-d-y", "y-M-d", "Y-M-d", "y-d-M", "Y-d-M"
    };

    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", commonDateTimeFormats)}\r\n{CalendarFormComponentProperties.CUSTOM_FORMAT_IDENTIFIER}";
}

internal class DefaultValueConfigurator : FormComponentConfigurator<CalendarFormComponent>
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
