using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.ComponentModel.DataAnnotations;

namespace Overhaul.AdminPanel.TagHelpers;


[HtmlTargetElement("InputDiv")]
public class InputTagHelper : TagHelper
{
    private IHtmlGenerator _htmlGenerator;

    public InputTagHelper(IHtmlGenerator htmlGenerator)
    {
        _htmlGenerator = htmlGenerator;
    }

    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // تولید تگ div با کلاس form-group
        output.TagName = "div";
        output.TagMode = TagMode.StartTagAndEndTag;
        output.Attributes.SetAttribute("class", "form-group");

        // پیدا کردن DisplayName از متادیتای ModelExpression
        string displayName = For.Metadata.DisplayName ?? For.Name;

        // پیدا کردن پیام Required از متادیتای ModelExpression
        string requiredMessage = GetRequiredMessage(For.Metadata);

        // ساختن تگ label
        var labelTag = new TagBuilder("label");
        labelTag.Attributes.Add("for", For.Name);
        labelTag.InnerHtml.Append(displayName);

        // ساختن تگ select
        var inputTag = new TagBuilder("input");
        inputTag.Attributes.Add("id", For.Name);
        inputTag.Attributes.Add("name", For.Name);
        inputTag.AddCssClass("form-control");

        // اگر پیام Required وجود دارد، تگ select باید ویژگی required را داشته باشد
        if (!string.IsNullOrEmpty(requiredMessage))
        {
            inputTag.Attributes.Add("required", "required");
        }


        var validationContext = CreateTagHelperContext();
        var validationOutput = CreateTagHelperOutput("span");

        var validation = new ValidationMessageTagHelper(_htmlGenerator)
        {
            For = For,
            ViewContext = ViewContext,

        };

        validation.Process(validationContext, validationOutput);
        validationOutput.Attributes.SetAttribute("class", "field-validation-error text-danger");
        validationOutput.Attributes.Add("data-valmsg-replace", "true");


        // تولید تگ span برای اعتبارسنجی
        var validationSpan = new TagBuilder("span");
        validationSpan.Attributes.Add("data-val", For.Name);
        validationSpan.Attributes.Add("asp-validation-for", For.Name);
        validationSpan.Attributes.Add("data-valmsg-for", For.Name);
        validationSpan.AddCssClass("field-validation-valid");
        validationSpan.AddCssClass("text-danger");
        validationSpan.Attributes.Add("data-val-required", requiredMessage);

        var validationMessage = $"<span class='text-danger' data-valmsg-for='{For.Name}' data-val='{For.Name}'>This field isvcbcvbcvb required.</span>";

        // اضافه کردن تگ‌ها به خروجی
        output.Content.AppendHtml(labelTag);
        output.Content.AppendHtml(inputTag);
        output.Content.AppendHtml(validationSpan);
    }

    private string GetRequiredMessage(ModelMetadata metadata)
    {
        foreach (var attribute in metadata.ValidatorMetadata)
        {
            if (attribute is RequiredAttribute requiredAttr)
            {
                return requiredAttr.ErrorMessage;
            }
        }
        return null;
    }
    private static TagHelperContext CreateTagHelperContext()
    {
        return new TagHelperContext(
            new TagHelperAttributeList(),
            new Dictionary<object, object>(),
            Guid.NewGuid().ToString("N"));
    }

    private static TagHelperOutput CreateTagHelperOutput(string tagName)
    {
        return new TagHelperOutput(
            tagName,
            new TagHelperAttributeList(),
            (a, b) =>
            {
                var tagHelperContent = new DefaultTagHelperContent();
                tagHelperContent.SetContent(string.Empty);
                return Task.FromResult<TagHelperContent>(tagHelperContent);
            });
    }
}
