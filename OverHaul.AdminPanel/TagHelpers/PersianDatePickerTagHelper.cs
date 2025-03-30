using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Overhaul.AdminPanel.TagHelpers;

[HtmlTargetElement("persian-date", Attributes = "asp-for")]
public class PersianDatePickerTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        base.Process(context, output);

        // اضافه کردن کلاس persian-datepicker به ورودی
        var classes = output.Attributes["class"]?.Value?.ToString();
        if (classes != null && !classes.Contains("persian-datepicker"))
        {
            output.Attributes.SetAttribute("class", classes + " persian-datepicker");
        }
        else
        {
            output.Attributes.SetAttribute("class", "form-control persian-datepicker");
        }

        // اضافه کردن ویژگی‌ها به خروجی
        output.Attributes.SetAttribute("data-valmsg-for", For.Name);
    }
}
