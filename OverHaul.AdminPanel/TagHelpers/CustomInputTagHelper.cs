using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Crypto;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Org.BouncyCastle.Bcpg.Sig;

namespace Overhaul.AdminPanel.TagHelpers;

[HtmlTargetElement("custom-input")]
public class CustomInputTagHelper : TagHelper
{
    [HtmlAttributeName("asp-for")]
    public ModelExpression For { get; set; }

    [HtmlAttributeName("id")]
    public string Id { get; set; }

    [HtmlAttributeName("bottstarp-col-number")]
    public int BottstarpColNumber { get; set; } = 6;

    [HtmlAttributeName("disable")]
    public bool Disable { get; set; } = false;

    [HtmlAttributeName("periasn-date")]
    public bool IsPersianDate { get; set; } = false;

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
        //columnDiv.AddCssClass($"column col-lg-{BottstarpColNumber} col-md-{BottstarpColNumber} col-sm-{BottstarpColNumber * 2}");
        var divWrapper = new TagBuilder("div");
        divWrapper.AddCssClass("input-group mb-4");
        var labaleSpanTag = new TagBuilder("span");
        labaleSpanTag.AddCssClass("input-group-text");
        labaleSpanTag.InnerHtml.Append(For.Metadata.DisplayName ?? For.Name).Append(" :");


        var inputType = "text"; // پیش‌فرض
        if (For.Metadata.ModelType == typeof(int) || For.Metadata.ModelType == typeof(float) ||
            For.Metadata.ModelType == typeof(double) || For.Metadata.ModelType == typeof(decimal))
        {
            inputType = "number";
        }
        else if (For.Metadata.ModelType == typeof(DateTime))
        {
            inputType = "date";
        }
        else if (For.Metadata.ModelType == typeof(bool))
        {
            inputType = "checkbox";
        }
        else if (For.Metadata.ModelType == typeof(string))
        {
            // اگر ویژگی [EmailAddress] روی فیلد رشته‌ای باشد، نوع input به email تغییر می‌یابد
            var emailAttribute = For.Metadata.ValidatorMetadata.OfType<EmailAddressAttribute>().FirstOrDefault();
            if (emailAttribute != null)
            {
                inputType = "email";
            }
        }
        // تولید input
        var inputTag = new TagBuilder("input");
        if (Disable == true)
        {
            inputTag.Attributes.Add("disabled", "disabled");
        }
        inputTag.Attributes.Add("name", For.Name);
        inputTag.Attributes.Add("id", For.Name);
        inputTag.Attributes.Add("type", inputType);
        inputTag.AddCssClass("form-control");
        if (IsPersianDate == true)
        {
            inputTag.AddCssClass("date-mask");
        }
        // افزودن ولیدیشن‌های ModelExpression به input
        int count = 0;
        foreach (var attribute in For.Metadata.ValidatorMetadata)
        {
            if (count++ == 0)
                inputTag.Attributes.Add("data-val", "true");

            if (attribute is RequiredAttribute requiredAttribute)
            {
                inputTag.Attributes.Add("data-val-required", requiredAttribute.ErrorMessage);
            }
            else if (attribute is RangeAttribute rangeAttribute)
            {
                inputTag.Attributes.Add("data-val-range", rangeAttribute.ErrorMessage);
                inputTag.Attributes.Add("data-val-range-min", rangeAttribute.Minimum.ToString());
                inputTag.Attributes.Add("data-val-range-max", rangeAttribute.Maximum.ToString());
               
                //inputTag.Attributes.Add("data-val-range-max", rangeAttribute.Minimum.ToString());
            }
            else if (attribute is MaxLengthAttribute maxLengthAttribute)
            {
                inputTag.Attributes.Add("data-val-maxlength", maxLengthAttribute.ErrorMessage);
                inputTag.Attributes.Add("data-val-maxlength-max", maxLengthAttribute.ErrorMessage);
            }
            //else if (attribute is RegularExpression regularExpression)
            //{
            //    inputTag.Attributes.Add("data-val-maxlength", regularExpression);
            //}
            else if (attribute is EmailAddressAttribute emailAttribute)
            {
                inputTag.Attributes.Add("data-val-email", emailAttribute.ErrorMessage);
            }
            else if (attribute is StringLengthAttribute stringLengthAttribute)
            {
                inputTag.Attributes.Add("data-val-length", stringLengthAttribute.ErrorMessage);
                inputTag.Attributes.Add("data-val-length-max", stringLengthAttribute.MaximumLength.ToString());
                if (stringLengthAttribute.MinimumLength > 0)
                {
                    inputTag.Attributes.Add("data-val-length-min", stringLengthAttribute.MinimumLength.ToString());
                }
            }
            else if (attribute is MaxLengthAttribute maxLengthAttribute1)
            {
                inputTag.Attributes.Add("data-val-maxlength", maxLengthAttribute1.ErrorMessage);
                inputTag.Attributes.Add("data-val-maxlength-max", maxLengthAttribute1.Length.ToString());
            }
            else if (attribute is LengthAttribute lengthAttr)
            {
                inputTag.Attributes.Add("data-val-maxlength", lengthAttr.ErrorMessage);
                }
            else if (attribute is DisplayFormatAttribute displayFormatAttribute)
            {
                if (!string.IsNullOrEmpty(displayFormatAttribute.DataFormatString))
                {
                    inputTag.Attributes.Add("data-val-display-format", displayFormatAttribute.DataFormatString);
                }
            }
        }

        // تنظیم مقدار input در صورت وجود مقدار در مدل
 


        // افزودن validation message
        var validationSpan = new TagBuilder("span");
        validationSpan.AddCssClass("text-danger babak");
        validationSpan.Attributes.Add("data-valmsg-for", For.Name);
        validationSpan.Attributes.Add("data-valmsg-replace", "true");
        validationSpan.AddCssClass("validation-error");
        validationSpan.Attributes.Add("onclick","$(\".span.validation-error\").hide()");
        validationSpan.Attributes.Add("tabindex", "-1");

        // اضافه کردن label، input و validation به داخل div
        var spanToolsTag = new TagBuilder("span");
        spanToolsTag.AddCssClass("input-group-text");

        var inputTag1 = new TagBuilder("input");

        if (For.Metadata.ModelType == typeof(bool))
        {
            // اضافه کردن فیلد مخفی برای ارسال مقدار false
            var hiddenInputCheckBox = new TagBuilder("input");
            hiddenInputCheckBox.Attributes.Add("type", "hidden");
            hiddenInputCheckBox.Attributes.Add("name", For.Name);
            hiddenInputCheckBox.Attributes.Add("value", "false");

            try
            {
                inputTag.Attributes.Add("data-val", "true");
                inputTag.Attributes.Add("data-val-required", "*");
            }
            catch (Exception)
            {


            }



            //
            var checkboxLabelTag= new TagBuilder("label");
            checkboxLabelTag.Attributes.Add("style", "width:100%;height: 30px;cursor: pointer;border-top-left-radius: 5px;border-bottom-left-radius: 5px;border-top-right-radius: 5px;border-bottom-right-radius: 5px;");
            checkboxLabelTag.Attributes.Add("for", For.Name);
            checkboxLabelTag.AddCssClass("input-group-text justify-content-between");

            if (Disable == true)
            {
                inputTag1.Attributes.Add("disabled=", "disabled");
            }
            inputTag1.Attributes.Add("name", For.Name);
            inputTag1.Attributes.Add("id", For.Name);
            inputTag1.Attributes.Add("type", inputType);
            inputTag1.Attributes.Add("style", "transform: scale(1.5);");
           
            checkboxLabelTag.InnerHtml.Append(For.Metadata.DisplayName ?? For.Name).Append(" :");

            checkboxLabelTag.InnerHtml.AppendHtml(inputTag1);
            divWrapper.InnerHtml.AppendHtml(checkboxLabelTag);
            divWrapper.InnerHtml.AppendHtml(hiddenInputCheckBox);
        }
        else
        {
            divWrapper.InnerHtml.AppendHtml(labaleSpanTag);
            divWrapper.InnerHtml.AppendHtml(inputTag);
          
        }

        if (For.Model != null)
        {
            var formattedValue = For.Model.ToString();
            foreach (var attribute in For.Metadata.ValidatorMetadata)
            {
                if (attribute is DisplayFormatAttribute displayFormatAttribute)
                {
                    formattedValue = string.Format(displayFormatAttribute.DataFormatString, For.Model);
                }
            }

            if (inputType == "checkbox")
            {
                ///  در چک باکس همیشه true میباشد
                inputTag1.Attributes.Add("value", "true");
                if (formattedValue.ToUpper().Trim() == "TRUE")
                {
                    inputTag1.Attributes.Add("checked", "checked");

                }
            }
            else
            {
                inputTag.Attributes.Add("value", formattedValue);
            }
        }

        inputTag.Attributes.Add("style", "border-bottom-left-radius: 0px;border-top-left-radius: 0px;");

        var clearButtonTag = new TagBuilder("button");
        clearButtonTag.AddCssClass("btn btn-sm border-end");
        clearButtonTag.Attributes.Add("type", "button");
        clearButtonTag.Attributes.Add("tabindex", "-1"); // جلوگیری از تب خوردن روی دکمه

        if (For.Metadata.ModelType != typeof(bool))
        {
            divWrapper.InnerHtml.AppendHtml(spanToolsTag);
            var clearButtonIconTag = new TagBuilder("i");
            clearButtonTag.AddCssClass("fa fa-times text-danger");

            clearButtonTag.InnerHtml.AppendHtml(clearButtonIconTag);
            clearButtonTag.Attributes.Add("onclick", "$(this).closest('.input-group').find('input').val('');");
            spanToolsTag.InnerHtml.AppendHtml(clearButtonTag);
            spanToolsTag.InnerHtml.AppendHtml(validationSpan);

        }
        columnDiv.InnerHtml.AppendHtml(divWrapper);

        // جایگزینی output با outerDiv و محتوای آن
        output.TagName = null; // حذف تگ اصلی
        output.Content.SetHtmlContent(columnDiv);
    }
}