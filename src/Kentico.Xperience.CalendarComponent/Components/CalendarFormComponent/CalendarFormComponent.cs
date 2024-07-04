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

    /// <summary>
    /// Sets time zone of the client.
    /// </summary>
    [BindableProperty]
    public string ClientOffsetToUtc { get; set; } = string.Empty;

    /// <inheritdoc />
    public override bool CustomAutopostHandling => true;

    /// <inheritdoc />
    public override DateTime GetValue()
    {
        var result = DateTime.Parse(SelectedDate, default);
        if (int.TryParse(ClientOffsetToUtc, out int jsUtcOffset))
        {
            var currentTimeZoneOffset = TimeZoneInfo.Local.GetUtcOffset(DateTime.UtcNow);

            int clientUtcOffset = -1 * jsUtcOffset;
            int jsHourUtcOffset = clientUtcOffset / 60;
            int difference = currentTimeZoneOffset.Hours - jsHourUtcOffset;

            return result.AddHours(difference);
        }
        return result;
    }

    /// <inheritdoc />
    public override void SetValue(DateTime value) =>
            SelectedDate = value.ToString("MM/dd/yyyy HH:mm:ss zzz");
}
