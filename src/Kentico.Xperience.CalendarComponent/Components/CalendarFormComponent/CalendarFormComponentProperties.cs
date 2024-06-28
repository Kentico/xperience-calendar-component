using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class CalendarFormComponentProperties : FormComponentProperties<DateTime>
{
    public const string CUSTOM_FORMAT_IDENTIFIER = "Custom";

    public CalendarFormComponentProperties() : base(FieldDataType.DateTime)
    {
    }

    [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Show Date Only", DefaultValue = false, ExplanationText = "Check for date-only selection. Displays date and time when unchecked", Order = 2)]
    public bool DateOnly { get; set; }

    [EditingComponent(IntInputComponent.IDENTIFIER, Label = "Time Slice", DefaultValue = 15, ExplanationText = "Select number of minutes in each individual time slice", Order = 3)]
    [VisibilityCondition(nameof(DateOnly), ComparisonTypeEnum.IsFalse)]
    public int TimeInterval { get; set; }

    [EditingComponent(DropDownComponent.IDENTIFIER, Label = "Date Time Format", DefaultValue = "m.d.Y", ExplanationText = "Select DateTime format", Order = 5)]
    [EditingComponentConfiguration(typeof(DateTimeFormatConfigurator), nameof(DateOnly))]
    public string DateTimeFormat { get; set; } = string.Empty;

    [DefaultValueEditingComponent(CalendarFormComponent.IDENTIFIER, Order = 10)]
    [EditingComponentConfiguration(typeof(DefaultValueConfigurator), nameof(DateTimeFormat))]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;
}

public class DateTimeFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonTimeFormats = new() { "H:i", "h:i" };
    private readonly List<string> commonDateTimeFormats = new() { "d.m.Y", "m.d.Y", "m/d/Y", "d/m/Y", "d.m.y", "m.d.y", "m/d/y", "d/m/y"
        ,"y.m.d", "y.d.m", "y/m/d", "y/d/m", "Y.m.d", "Y.d.m", "Y/m/d", "Y/d/m"
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

        formComponent.Properties.DateOnly = dateOnly;
        formComponent.Properties.DateTimeFormat = dateTimeFormat;
        formComponent.Properties.TimeInterval = timeInterval;
    }
}
