using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Overhaul.AdminPanel.TagHelpers;


[HtmlTargetElement("checkbox", Attributes = ForAttributeName)]
public class CheckBoxTagHelper : TagHelper
{
    private const string ForAttributeName = "asp-for";

    [HtmlAttributeName(ForAttributeName)]
    public ModelExpression For { get; set; }

    [HtmlAttributeName("bottstarp-col-number")]
    public int BottstarpColNumber { get; set; } = 6;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // تنظیم div اصلی با کلاس bootstrap
        var outerDiv = new TagBuilder("div");
        outerDiv.AddCssClass($"col-md-{BottstarpColNumber}");

        var divWrapper = new TagBuilder("div");
        divWrapper.AddCssClass("form-group form-check");

        var hiddenInput = new TagBuilder("input");
        hiddenInput.Attributes.Add("type", "hidden");
        hiddenInput.Attributes.Add("name", For.Name);
        hiddenInput.Attributes.Add("value", "false");

        // ایجاد input چک‌باکس
        var inputTag = new TagBuilder("input");
        inputTag.Attributes.Add("type", "checkbox");
        inputTag.Attributes.Add("class", "form-check-input");
        inputTag.Attributes.Add("id", For.Name);
        inputTag.Attributes.Add("name", For.Name);
        inputTag.Attributes.Add("data-val", "true");
        inputTag.Attributes.Add("data-val-required", "*");
        inputTag.Attributes.Add("value", "true");
        // بررسی مقدار true برای checked
        if (For.Model is bool boolValue && boolValue)
        {
            inputTag.Attributes.Add("checked", "checked");
        }
       

        var labelTag = new TagBuilder("label");
        labelTag.Attributes.Add("for", For.Name);
        labelTag.AddCssClass("form-check-label");
        labelTag.InnerHtml.Append(For.Metadata.DisplayName ?? For.Name);

     

        // اضافه کردن چک‌باکس و لیبل به div
      
        divWrapper.InnerHtml.AppendHtml(inputTag);
        divWrapper.InnerHtml.AppendHtml(labelTag);
        divWrapper.InnerHtml.AppendHtml(hiddenInput);
        outerDiv.InnerHtml.AppendHtml(divWrapper);

        output.TagName = null;
        output.Content.SetHtmlContent(outerDiv);
    }
}