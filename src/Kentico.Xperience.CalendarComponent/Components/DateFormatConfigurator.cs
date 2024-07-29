using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components;

internal class DateFormatConfigurator : FormComponentConfigurator<DropDownComponent>
{
    private readonly List<string> commonDateTimeFormats = new() {
        "d.M.Y", "M.d.Y", "M/d/Y", "d/M/Y", "d.M.y", "M.d.y", "M/d/y", "d/M/y"
        ,"y.M.d", "y.d.M", "y/M/d", "y/d/M", "Y.M.d", "Y.d.M", "Y/M/d", "Y/d/M",
        "d-M-Y", "d-M-y", "M-d-Y", "M-d-y", "y-M-d", "Y-M-d", "y-d-M", "Y-d-M"
    };

    public override void Configure(DropDownComponent formComponent, IFormFieldValueProvider formFieldValueProvider)
        => formComponent.Properties.DataSource = $"{string.Join("\r\n", commonDateTimeFormats)}";
}
