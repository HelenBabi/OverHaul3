using Humanizer;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Drawing;
using System.Security.Policy;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Reflection;
using System.Reflection.Emit;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;
using Autofac;

namespace Overhaul.AdminPanel.TagHelpers;



[HtmlTargetElement("SelectedLists")]
public class SelectListTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }
    public SelectList Items { get; set; }
    public string PlaceholderText { get; set; } = "انتخاب کنید";

    [HtmlAttributeName("dropdown-parent")]
    public string dropdownParent { get; set; } = "";

    [HtmlAttributeName("bottstarp-col-number")]
    public int BottstarpColNumber { get; set; } = 6;

    [HtmlAttributeName("disable")]
    public bool Disable { get; set; } = false;

    //[HtmlAttributeName("multi-select")]
    //public bool IsMultiSelect { get; set; } = false; // ویژگی جدید برای چند انتخابی

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var columnDiv = new TagBuilder("div");
        if (BottstarpColNumber == 1)
        {
            columnDiv.AddCssClass($"column col-lg-{1} col-md-{2} col-sm-{3}");
        }
        else if (BottstarpColNumber == 2)
        {
            columnDiv.AddCssClass($"column col-lg-{2} col-md-{3} col-sm-{4}");
        }
        else if (BottstarpColNumber == 3)
        {
            columnDiv.AddCssClass($"column col-lg-{3} col-md-{3} col-sm-{6}");
        }
        else if (BottstarpColNumber == 4)
        {
            columnDiv.AddCssClass($"column col-lg-{4} col-md-{6} col-sm-{12}");
        }
        else if (BottstarpColNumber == 5)
        {
            columnDiv.AddCssClass($"column col-lg-{5} col-md-{6} col-sm-{12}");
        }
        else if (BottstarpColNumber == 6)
        {
            columnDiv.AddCssClass($"column col-lg-{6} col-md-{6} col-sm-{12}");
        }
        else if (BottstarpColNumber == 7)
        {
            columnDiv.AddCssClass($"column col-lg-{7} col-md-{7} col-sm-{12}");
        }
        else if (BottstarpColNumber == 8)
        {
            columnDiv.AddCssClass($"column col-lg-{8} col-md-{12} col-sm-{12}");
        }
        else if (BottstarpColNumber == 9)
        {
            columnDiv.AddCssClass($"column col-lg-{9} col-md-{12} col-sm-{12}");
        }
        else if (BottstarpColNumber == 10)
        {
            columnDiv.AddCssClass($"column col-lg-{10} col-md-{12} col-sm-{12}");
        }
        else if (BottstarpColNumber == 11)
        {
            columnDiv.AddCssClass($"column col-lg-{11} col-md-{12} col-sm-{12}");
        }
        else
        {
            columnDiv.AddCssClass($"column col-lg-{12} col-md-{12} col-sm-{12}");
        }
        //columnDiv.AddCssClass($"column col-md-{BottstarpColNumber} col-sm-{BottstarpColNumber * 2}");

        var divWrapper = new TagBuilder("div");
        divWrapper.AddCssClass("input-group mb-4");
        divWrapper.Attributes.Add("style", "flex-wrap: unset; !important;");

        string displayName = For.Metadata.DisplayName ?? For.Name;
        string requiredMessage = GetRequiredMessage(For.Metadata);

        var labelTag = new TagBuilder("label");
        labelTag.AddCssClass("input-group-text");
        labelTag.Attributes.Add("for", For.Name);
        labelTag.InnerHtml.Append(displayName).Append(" :");

        var selectTag = new TagBuilder("select");
        selectTag.Attributes.Add("id", For.Name);
        selectTag.Attributes.Add("name", For.Name);
        selectTag.AddCssClass("form-select");

        string ClassName = (dropdownParent.Length > 0) ? "SelectList-Select2-Modal" : "SelectList-Select2";
        selectTag.AddCssClass(ClassName);

        if (Disable)
        {
            selectTag.Attributes.Add("disabled", "disabled");
        }

        if (!string.IsNullOrEmpty(requiredMessage))
        {
            selectTag.Attributes.Add("required", "required");
            selectTag.Attributes.Add("data-val-required", requiredMessage);
            selectTag.Attributes.Add("data-val", "true");
        }

        if (PlaceholderText.Length > 0)
        {
            var placeholderOption = new TagBuilder("option");
            placeholderOption.Attributes.Add("value", "");
            placeholderOption.InnerHtml.Append(PlaceholderText);
            selectTag.InnerHtml.AppendHtml(placeholderOption);
        }

        foreach (var item in Items)
        {
            var optionTag = new TagBuilder("option");
            optionTag.Attributes.Add("value", item.Value);
            if (item.Selected)
            {
                optionTag.Attributes.Add("selected", "selected");
            }
            optionTag.InnerHtml.Append(item.Text);
            selectTag.InnerHtml.AppendHtml(optionTag);
        }

        var spanToolsTag = new TagBuilder("span");
        spanToolsTag.AddCssClass("input-group-text");

        var clearButtonTag = new TagBuilder("button");
        clearButtonTag.AddCssClass("btn btn-sm border-end");
        clearButtonTag.Attributes.Add("type", "button");
        clearButtonTag.Attributes.Add("tabindex", "-1"); // جلوگیری از فوکوس شدن دکمه حذف
        var clearButtonIconTag = new TagBuilder("i");
        clearButtonTag.AddCssClass("fa fa-times text-danger");

        clearButtonTag.InnerHtml.AppendHtml(clearButtonIconTag);
        clearButtonTag.Attributes.Add("onclick", "$(this).closest('.input-group').find('select').val(null).trigger('change');");

        spanToolsTag.InnerHtml.AppendHtml(clearButtonTag);

        var validationSpan = new TagBuilder("span");
        validationSpan.Attributes.Add("asp-validation-for", For.Name);
        validationSpan.Attributes.Add("data-valmsg-for", For.Name);
        validationSpan.AddCssClass("field-validation-valid text-danger");
        validationSpan.Attributes.Add("tabindex", "-1"); // جلوگیری از فوکوس شدن پیام خطا

        var modalname = (dropdownParent.Length > 0) ? $@"dropdownParent: $('#{dropdownParent}')" : "";
        //var multipleSelect = IsMultiSelect ? "multiple: true," : "";

        var script = $@"
        <script>
           $(document).ready(function () {{
                $(""#{For.Name}"").select2({{
                    dir: ""rtl"",
                    width: '100%',
                    {modalname} 
                }});
           }});
        </script> ";

        divWrapper.InnerHtml.AppendHtml(labelTag);
        divWrapper.InnerHtml.AppendHtml(selectTag);
        spanToolsTag.InnerHtml.AppendHtml(validationSpan);
        divWrapper.InnerHtml.AppendHtml(spanToolsTag);
        columnDiv.InnerHtml.AppendHtml(divWrapper);
        output.TagName = null;
        output.Content.SetHtmlContent(columnDiv);
        //output.PostContent.SetHtmlContent(script);
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
}




