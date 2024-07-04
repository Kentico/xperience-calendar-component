using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components;

[assembly: RegisterFormComponent(
    identifier: CalendarFormComponent.IDENTIFIER,
    formComponentType: typeof(CalendarFormComponent),
    name: "Calendar",
    Description = "Pick date from calendar",
    IconClass = "icon-calendar",
    ViewName = "~/Components/CalendarFormComponent/_CalendarFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components;

/// <summary>
/// Class which constructs the Calendar Form Component.
/// </summary>
public class CalendarFormComponent : FormComponent<CalendarFormComponentProperties, DateTime>
{
    /// <summary>
    /// The internal identifier of this component.
    /// </summary>
    public const string IDENTIFIER = nameof(CalendarFormComponent);

    /// <summary>
    /// Selected value in the form component. Later processed and saved in the database.
    /// </summary>
    [BindableProperty]
    public string SelectedDate { get; set; } = "";

    /// <inheritdoc />
    public override bool CustomAutopostHandling => true;

    /// <inheritdoc />
    public override DateTime GetValue() => DateTime.Parse(SelectedDate, default);

    /// <inheritdoc />
    public override void SetValue(DateTime value) => SelectedDate = value.ToString();
}
