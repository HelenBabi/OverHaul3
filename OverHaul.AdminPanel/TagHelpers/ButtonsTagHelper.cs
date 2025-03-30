using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Overhaul.AdminPanel.TagHelpers;

[HtmlTargetElement("ButtonSave")]
public class ButtonSaveTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-primary btn-md");
        output.Attributes.SetAttribute("type", "submit");
        output.Content.SetHtmlContent("<span class=\"ti-save\" style=\"margin-left: 10px;\"></span>ثبت");
    }
}
[HtmlTargetElement("ButtonEditSave")]
public class ButtonEditSaveTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-warning btn-md");
        output.Attributes.SetAttribute("type", "submit");
        output.Content.SetHtmlContent("<span class=\"ti-save\" style=\"margin-left: 10px;\"></span> ثبت ویرایش");
    }
}

[HtmlTargetElement("ButtonAdd")]
public class ButtonAddTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-primary btn-md");
        output.Attributes.SetAttribute("type", "button");
        output.Content.SetHtmlContent("<span class=\"ti-plus\"></span>");
    }
}


[HtmlTargetElement("ButtonBack")]
public class ButtonBackTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-info btn-md");
        output.Attributes.SetAttribute("type", "button");
        output.Content.SetHtmlContent("<span class=\"ti-back-left\"</span>");
        output.Attributes.SetAttribute("onclick", "location.href='/Home/index'");
    }
}

[HtmlTargetElement("ButtonBackModal")]
public class ButtonBackModalTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-info btn-md");
        output.Attributes.SetAttribute("type", "button");
        output.Content.SetHtmlContent("<span class=\"ti-back-left\" style=\"margin-left: 10px;\"></span>بازگشت");
        output.Attributes.SetAttribute("data-bs-dismiss", "modal");
    }
}


[HtmlTargetElement("ButtonDelete")]
public class ButtonDeleteTagHelper : TagHelper
{
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-danger btn-md");
        output.Attributes.SetAttribute("type", "submit");
        output.Content.SetHtmlContent("<span class=\"ti-trash\" style=\"margin-left: 10px;\"></span>حذف");
    }
}

[HtmlTargetElement("ButtonReport")]
public class ButtonReportTagHelper : TagHelper
{
    [HtmlAttributeName("title")]
    public string Title { get; set; } = "گزارش";
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "button";
        output.Attributes.SetAttribute("class", "btn btn-success btn-md");
        output.Attributes.SetAttribute("type", "button");
        output.Content.SetHtmlContent($"<span class=\"ti-bar-chart\" style=\"margin-left: 10px;\"></span>{Title}");
    }
}




