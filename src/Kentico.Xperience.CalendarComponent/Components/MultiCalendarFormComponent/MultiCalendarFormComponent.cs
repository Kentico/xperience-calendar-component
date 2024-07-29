using System.Text;

using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components;

[assembly: RegisterFormComponent(
    identifier: MultiCalendarFormComponent.IDENTIFIER,
    formComponentType: typeof(MultiCalendarFormComponent),
    name: "Multi-Value Calendar",
    Description = "Pick date range from calendar",
    IconClass = "icon-calendar",
    ViewName = "~/Components/MultiCalendarFormComponent/_MultiCalendarFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components;

/// <summary>
/// Class which constructs the Calendar Form Component with multiple selectable days or a range of dates.
/// </summary>
public class MultiCalendarFormComponent : FormComponent<MultiCalendarFormComponentProperties, string>
{
    /// <summary>
    /// The internal identifier of this component.
    /// </summary>
    public const string IDENTIFIER = nameof(MultiCalendarFormComponent);

    /// <summary>
    /// Selected value in the form component. Later processed and saved in the database.
    /// </summary>
    [BindableProperty]
    public string SelectedValue { get; set; } = string.Empty;

    /// <inheritdoc />
    public override bool CustomAutopostHandling => true;

    /// <inheritdoc />
    public override string GetValue() => SelectedValue;

    /// <inheritdoc />
    public override void SetValue(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            SelectedValue = value;
            return;
        }

        var stringBuilder = new StringBuilder();

        string[] tokens;

        if (value.Contains("to"))
        {
            tokens = value.Split("to", StringSplitOptions.TrimEntries);

            if (tokens.Length != 2)
            {
                throw new ArgumentException("Incorect number of dates in range.");
            }
        }
        else
        {
            char separator = GetSeparator(value);
            tokens = value.Split(separator, StringSplitOptions.TrimEntries);
        }

        if (DateOnly.TryParse(tokens[0], default, System.Globalization.DateTimeStyles.None, out var firstDate))
        {
            stringBuilder.Append(firstDate);
        }
        else
        {
            throw new ArgumentException($"{firstDate} is not a correct date");
        }

        for (int i = 1; i < tokens.Length; i++)
        {
            if (DateOnly.TryParse(tokens[i], default, System.Globalization.DateTimeStyles.None, out var dateOnly))
            {
                stringBuilder.Append(';');
                stringBuilder.Append(dateOnly);
            }
            else
            {
                throw new ArgumentException($"{dateOnly} is not a correct date");
            }
        }

        SelectedValue = stringBuilder.ToString();
    }

    private char GetSeparator(string value)
    {
        if (value.Contains(';'))
        {
            return ';';
        }
        else
        {
            return ',';
        }
    }
}
