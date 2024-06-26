using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class DatePickerFormComponentProperties : FormComponentProperties<DateTime>
{
    public DatePickerFormComponentProperties() : base(FieldDataType.DateTime)
    {
    }

    [EditingComponent(TextInputComponent.IDENTIFIER, Label = "Date Format", DefaultValue = "m.d.Y", ExplanationText = "Enter date format. Examples: d.m.Y, Y/m/d")]
    public string DateFormat { get; set; } = string.Empty;

    [EditingComponent(CheckBoxComponent.IDENTIFIER, Label = "Show Date Only", DefaultValue = false, ExplanationText = "Check for date-only selection. Displays date and time when unchecked")]
    public bool DateOnly { get; set; }

    [DefaultValueEditingComponent(DatePickerFormComponent.IDENTIFIER)]
    public override DateTime DefaultValue { get; set; } = DateTime.Now;
}
