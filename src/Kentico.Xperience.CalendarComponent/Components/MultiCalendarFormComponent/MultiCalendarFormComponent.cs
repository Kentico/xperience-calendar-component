using System.Text;

using Kentico.Forms.Web.Mvc;
using Kentico.Xperience.CalendarComponent.Components.FormComponents;

[assembly: RegisterFormComponent(
    identifier: MultiCalendarFormComponent.IDENTIFIER,
    formComponentType: typeof(MultiCalendarFormComponent),
    name: "Multi Calendar",
    Description = "Pick date range from calendar",
    ViewName = "~/Components/MultiCalendarFormComponent/_MultiCalendarFormComponent.cshtml")]

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class MultiCalendarFormComponent : FormComponent<MultiCalendarFormComponentProperties, string>
{
    public const string IDENTIFIER = nameof(MultiCalendarFormComponent);

    [BindableProperty]
    public string SelectedValue { get; set; } = string.Empty;

    public override bool CustomAutopostHandling => true;

    public override string GetValue() => SelectedValue;

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

        if (DateOnly.TryParse(tokens[0], out var firstDate))
        {
            stringBuilder.Append(firstDate);
        }
        else
        {
            throw new ArgumentException($"{firstDate} is not a correct date");
        }

        for (int i = 1; i < tokens.Length; i++)
        {
            if (DateOnly.TryParse(tokens[i], out var dateOnly))
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
