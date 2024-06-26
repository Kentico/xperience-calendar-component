using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components.FormComponents;

[assembly: RegisterFormComponent(
    identifier: DatePickerFormComponent.IDENTIFIER,
    formComponentType: typeof(DatePickerFormComponent),
    name: "DatePicker",
    Description = "Pick date from calendar",
    ViewName = "~/Components/FormComponents/_DatePickerFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class DatePickerFormComponent : FormComponent<DatePickerFormComponentProperties, DateTime>
{
    public const string IDENTIFIER = nameof(DatePickerFormComponent);

    [BindableProperty]
    public string SelectedDate { get; set; } = "";

    public override bool CustomAutopostHandling => true;
    public override DateTime GetValue() => DateTime.Parse(SelectedDate);
    public override void SetValue(DateTime value) => SelectedDate = value.ToString();
}
