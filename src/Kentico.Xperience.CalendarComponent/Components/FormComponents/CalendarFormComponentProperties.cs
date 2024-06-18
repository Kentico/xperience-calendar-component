using CMS.DataEngine;

using Kentico.Forms.Web.Mvc;

namespace Kentico.Xperience.CalendarComponent.Components.FormComponents;

public class CalendarFormComponentProperties : FormComponentProperties<DateOnly>
{
    public CalendarFormComponentProperties() : base(FieldDataType.Date)
    {
    }

    [DefaultValueEditingComponent(default)]
    public override DateOnly DefaultValue { get; set; }
}
