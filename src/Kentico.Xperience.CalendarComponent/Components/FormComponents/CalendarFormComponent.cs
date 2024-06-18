using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components.FormComponents;

[assembly: RegisterFormComponent(
    identifier: CalendarFormComponent.IDENTIFIER,
    formComponentType: typeof(CalendarFormComponent),
    name: "{$kentico.xperience.formbuilder.component.formcomponent.name$}",
    Description = "{$kentico.xperience.formbuilder.component.formcomponent.description$}",
    IconClass = "",
    ViewName = "~/Components/FormComponents/CalendarFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class CalendarFormComponent : FormComponent<CalendarFormComponentProperties, DateOnly>
{
    public const string IDENTIFIER = nameof(CalendarFormComponent);

    public DateOnly Date { get; set; }

    public override bool CustomAutopostHandling => true;

    public override DateOnly GetValue() => Date;

    public override void SetValue(DateOnly value) => Date = value;
}
