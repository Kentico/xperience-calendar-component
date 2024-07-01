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


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Date Time Format",
        DefaultValue = "m.d.Y",
        ExplanationText = "Select DateTime format",
        Order = 5)]
    [EditingComponentConfiguration(typeof(DateTimeFormatConfigurator), nameof(DateOnly))]
    public string DateTimeFormat { get; set; } = string.Empty;


    [EditingComponent(DropDownComponent.IDENTIFIER,
        Label = "Excluded Date Time Data Provider",
        DefaultValue = NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER,
        ExplanationText = "Select a provider for excluded date and time frames. Choose \"None\" option for no provider.")]
    [EditingComponentConfiguration(typeof(ExcludedDateTimeProviderConfigurator))]
    public string ExcludedDateTimeDataProvider { get; set; } = string.Empty;


    [DefaultValueEditingComponent(CalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(DefaultValueConfigurator), nameof(DateTimeFormat))]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;
}

public class DateTimeFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonTimeFormats = new() { "H:i", "h:i" };
    private readonly List<string> commonDateTimeFormats = new() {
        "d.m.Y", "m.d.Y", "m/d/Y", "d/m/Y", "d.m.y", "m.d.y", "m/d/y", "d/m/y"
        ,"y.m.d", "y.d.m", "y/m/d", "y/d/m", "Y.m.d", "Y.d.m", "Y/m/d", "Y/d/m",
        "d-m-Y", "d-m-y", "m-d-Y", "m-d-y", "y-m-d", "Y-m-d", "y-d-m", "Y-d-m"
    };

    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
    {
        bool dateOnly = IsDateOnly(formFieldValueProvider);

        if (dateOnly)
        {
            formComponent.Properties.DataSource = $"{string.Join("\r\n", commonDateTimeFormats)}\r\n{CalendarFormComponentProperties.CUSTOM_FORMAT_IDENTIFIER}";
        }
        else
        {
            var combinations = new List<string>();

            foreach (string date in commonDateTimeFormats)
            {
                foreach (string time in commonTimeFormats)
                {
                    combinations.Add($"{date} {time}");
                }
            }

            formComponent.Properties.DataSource = $"{string.Join("\r\n", combinations)}\r\n{CalendarFormComponentProperties.CUSTOM_FORMAT_IDENTIFIER}";
        }
    }
    private bool IsDateOnly(IFormFieldValueProvider formFieldValueProvider)
    {
        if (formFieldValueProvider.TryGet(DependencyFieldName, out bool dateOnly))
        {
            return dateOnly;
        }

        return false;
    }
}

public class DefaultValueConfigurator : FormComponentConfigurator<CalendarFormComponent>
{
    public override void Configure(CalendarFormComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
    {
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.DateOnly), out bool dateOnly);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.DateTimeFormat), out string dateTimeFormat);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.TimeInterval), out int timeInterval);
        formFieldValueProvider.TryGet(nameof(CalendarFormComponentProperties.ExcludedDateTimeDataProvider), out string dateTimeDataProvider);

        formComponent.Properties.DateOnly = dateOnly;
        formComponent.Properties.DateTimeFormat = dateTimeFormat;
        formComponent.Properties.TimeInterval = timeInterval;
        formComponent.Properties.ExcludedDateTimeDataProvider = dateTimeDataProvider;
    }
}

internal class ExcludedDateTimeProviderConfigurator : FormComponentConfigurator<DropDownComponent>
{
    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", CalendarProviderStorage.Providers.Keys)}\r\n{CalendarFormComponentProperties.NO_EXCLUDED_DATETIME_DATA_PROVIDER_IDENTIFIER}";
}
