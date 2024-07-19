using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components;

/// <summary>
/// The properties to be set in the Kentico administration to configure the calendar multi-value form component.
/// </summary>
public class MultiCalendarFormComponentProperties : FormComponentProperties<string>
{
    /// <summary>
    /// Identifier of no dynamic data provider.
    /// </summary>
    internal const string NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER = "None";

    /// <summary>
    /// Initializes a new instance of Kentico.Xperience.CalendarComponent.Components.MultiCalendarFormComponentProperties
    /// </summary>
    public MultiCalendarFormComponentProperties() : base(FieldDataType.Text, size: 200)
    {
    }

    /// <summary>
    /// Configures whether the component should set multiple dates or a range of dates.
    /// </summary>
    [EditingComponent(CheckBoxComponent.IDENTIFIER,
        Label = "Is multi date selection",
        DefaultValue = false,
        ExplanationText = "Check for multi value selection. Uncheck for Range selection.",
        Order = 1)]
    public bool IsMulti { get; set; }

    /// <summary>
    /// Configures displayed date format.
    /// </summary>
    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Date Format",
        DefaultValue = "m.d.Y",
        ExplanationText = "Select Date format",
        Order = 2)]
    [EditingComponentConfiguration(typeof(DateFormatConfigurator))]
    public string DateFormat { get; set; } = string.Empty;

    /// <summary>
    /// Configures the dynamic calendar data provider.
    /// </summary>
    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded dates. Choose \"None\" for no provider.",
        Order = 3)]
    [EditingComponentConfiguration(typeof(MultiCalendarExcludedDateTimeDataProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;

    /// <summary>
    /// Sets the default value for calendar component.
    /// </summary>
    [DefaultValueEditingComponent(MultiCalendarFormComponent.IDENTIFIER, Order = 4)]
    [EditingComponentConfiguration(typeof(MultiCalendarDefaultValueConfigurator), nameof(DateFormat))]
    public override string DefaultValue { get; set; } = string.Empty;
}
