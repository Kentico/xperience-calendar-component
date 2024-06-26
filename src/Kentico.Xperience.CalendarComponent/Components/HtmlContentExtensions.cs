using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Kentico.Xperience.CalendarComponent.Components;

public static class HtmlContentExtensions
{
    public static IHtmlContent CustomInput(this IHtmlHelper helper, string id, string inputType, string name, object value, IDictionary<string, object?> htmlAttributes)
    {
        var tagBuilder = new TagBuilder("input")
        {
            TagRenderMode = TagRenderMode.StartTag
        };

        tagBuilder.MergeAttribute("type", inputType);
        tagBuilder.MergeAttribute("id", id);
        tagBuilder.MergeAttribute("name", name);
        tagBuilder.MergeAttribute("value", value.ToString());

        tagBuilder.MergeAttributes(htmlAttributes);

        using var writer = new StringWriter();
        tagBuilder.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }
}
