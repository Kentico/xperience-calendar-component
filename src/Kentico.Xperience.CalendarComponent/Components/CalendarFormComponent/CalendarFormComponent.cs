using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components.FormComponents;

[assembly: RegisterFormComponent(
    identifier: CalendarFormComponent.IDENTIFIER,
    formComponentType: typeof(CalendarFormComponent),
    name: "Calendar",
    Description = "Pick date from calendar",
    ViewName = "~/Components/CalendarFormComponent/_CalendarFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class CalendarFormComponent : FormComponent<CalendarFormComponentProperties, DateTime>
{
    public const string IDENTIFIER = nameof(CalendarFormComponent);

    [BindableProperty]
    public string SelectedDate { get; set; } = "";

    public override bool CustomAutopostHandling => true;
    public override DateTime GetValue() => DateTime.Parse(SelectedDate);
    public override void SetValue(DateTime value) => SelectedDate = value.ToString();
}
