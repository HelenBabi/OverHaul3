using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.Rendering;

public static class EnumExtensions
{
    public static SelectList GetEnumSelectList<TEnum>(object selectedValue = null) where TEnum : Enum
    {
        var values = Enum.GetValues(typeof(TEnum))
            .Cast<TEnum>()
            .Select(e => new
            {
                Value = Convert.ToInt32(e),
                Text = GetEnumDescription(e)
            }).ToList();

        return new SelectList(values, "Value", "Text", selectedValue);
    }

    private static string GetEnumDescription<TEnum>(TEnum value) where TEnum : Enum
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = (DescriptionAttribute)Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute));
        return attribute?.Description ?? value.ToString();
    }
}
