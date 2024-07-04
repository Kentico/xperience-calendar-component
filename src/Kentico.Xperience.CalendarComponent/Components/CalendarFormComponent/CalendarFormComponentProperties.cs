using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components;

/// <summary>
/// The properties to be set in the Kentico administration to configure the calendar form component.
/// </summary>
public class CalendarFormComponentProperties : FormComponentProperties<DateTime>
{
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
    [EditingComponentConfiguration(typeof(DateFormatConfigurator), nameof(DateOnly))]
    public string DateFormat { get; set; } = string.Empty;

    /// <summary>
    /// Configures the dynamic calendar data provider.
    /// </summary>
    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded date and time frames. Choose \"None\" option for no provider.")]
    [EditingComponentConfiguration(typeof(CalendarExcludedDateTimeDataProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;

    /// <summary>
    /// Sets the default value for calendar component.
    /// </summary>
    [DefaultValueEditingComponent(CalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(CalendarDefaultValueConfigurator), nameof(DateFormat))]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;

    /// <summary>
    /// Configures whether the value should be automatically shown in the client's time zone. If true,
    /// offset is added to the selected time according to user's time zone.
    /// Time is always saved in the server's time zone.
    /// Only Date Time uses the offset. Date only does not convert.
    /// </summary>
    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Order = 11,
        Label = "Display time in client's time zone",
        DefaultValue = true,
        ExplanationText = "Configures whether the value should be automatically shown in the client's time zone. If true, offset is added to the selected time according to user's time zone. Time is always saved in the server's time zone.")]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public bool DisplayTimeInClientTimeZone { get; set; }
}
