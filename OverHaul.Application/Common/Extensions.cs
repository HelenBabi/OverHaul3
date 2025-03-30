using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Overhaul.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Overhaul.Application;

public static partial class Extension1
{
    public static string MobileWithZero(this string mobile)
    {
        if (mobile.Length == 11)
        {
            return mobile;
        }
        else return $"0{mobile}";
    }
    public static string MobileWithoutZero(this string mobile)
    {
        if (mobile.Length == 11)
        {
            return mobile.Substring(1);
        }
        else return mobile;
    }
    public static string GetPersianDate(this string date_)
    {
        PersianCalendar PDate = new PersianCalendar();
        string y, m, d;
        y = Convert.ToString(PDate.GetYear(DateTime.Now));
        m = Convert.ToString(PDate.GetMonth(DateTime.Now));
        if (m.Length == 1)
            m = "0" + m;
        d = Convert.ToString(PDate.GetDayOfMonth(DateTime.Now));
        if (d.Length == 1)
            d = "0" + d;
        return y + "/" + m + "/" + d;
    }


    public static string GetFileReal(string FileHeaderHex)
    {
        string output = "";
        switch (FileHeaderHex)
        {
            case "38-42-50-53":
                output = ">PSD";
                break;
            case "89-50-4E-47":
                output = ".PNG";
                break;
            case "25-50-44-46":
                output = ".PDF";
                break;
            case "49-49-2A-00":
                output = ">TIF";
                break;
            case "4D-4D-00-2A":
                output = ">TIF";
                break;
            case "50-4B-03-04-14":
                output = ".XLSX";
                break;
            case "42-4D-46-EB":
                output = ".BMP";
                break;
            case "42-4D-0E-09":
                output = ".BMP";
                break;
            case "42-4D-00-B0":
                output = ".BMP";
                break;
            case "42-4D-C8-5F":
                output = ".BMP";
                break;
            case "42-4D-F4-08":
                output = ".BMP";
                break;

            case "null":
                output = "file type is not matches with array";
                break;
        }
        return output;
    }

    public static string Fa2En(this string str)
    {
        return str.Replace("۰", "0")
            .Replace("۱", "1")
            .Replace("۲", "2")
            .Replace("۳", "3")
            .Replace("۴", "4")
            .Replace("۵", "5")
            .Replace("۶", "6")
            .Replace("۷", "7")
            .Replace("۸", "8")
            .Replace("۹", "9")
            //iphone numeric
            .Replace("٠", "0")
            .Replace("١", "1")
            .Replace("٢", "2")
            .Replace("٣", "3")
            .Replace("٤", "4")
            .Replace("٥", "5")
            .Replace("٦", "6")
            .Replace("٧", "7")
            .Replace("٨", "8")
            .Replace("٩", "9");
    }


    public static string GetExcelColumnName(this int ColumnCount)
    {
        //string  Cname = "";
        if (ColumnCount == 1)
            return "A";
        else if (ColumnCount == 2)
            return "B";
        else if (ColumnCount == 3)
            return "C";
        else if (ColumnCount == 4)
            return "D";
        else if (ColumnCount == 5)
            return "E";
        else if (ColumnCount == 6)
            return "F";
        else if (ColumnCount == 7)
            return "G";
        else if (ColumnCount == 8)
            return "H";
        else if (ColumnCount == 9)
            return "I";
        else if (ColumnCount == 10)
            return "J";
        else if (ColumnCount == 11)
            return "K";
        else if (ColumnCount == 12)
            return "L";
        else if (ColumnCount == 13)
            return "M";
        else if (ColumnCount == 14)
            return "N";
        else if (ColumnCount == 15)
            return "O";
        else if (ColumnCount == 16)
            return "P";
        else if (ColumnCount == 17)
            return "Q";
        else if (ColumnCount == 18)
            return "R";
        else if (ColumnCount == 19)
            return "S";
        else if (ColumnCount == 20)
            return "T";
        else if (ColumnCount == 21)
            return "U";
        else if (ColumnCount == 22)
            return "V";
        else if (ColumnCount == 23)
            return "W";
        else if (ColumnCount == 24)
            return "X";
        else if (ColumnCount == 25)
            return "Y";
        else if (ColumnCount == 26)
            return "Z";

        else
            return "";




    }

    public static Int64 NumberFormatToInt64(this string Value)
    {
        Value = Value.Trim().Fa2En().Replace(",", "");
        try
        {
            return Convert.ToInt64(Value);
        }
        catch (Exception)
        {
            return 0;
        }
        
    }
    public static Int32 NumberFormatToInt32(this string Value)
    {
        Value = Value.Trim().Fa2En().Replace(",", "");
        if (Value == null || Value.Length == 0)
            return 0;

        return Convert.ToInt32(Value);
    }
    public static bool IsNullOrZero(this int? value)
    {
        return value is null or 0;
    }
    public static bool HasItems<T>(this List<T> src, List<T> des)
    {
        if (src.Any() && !(src.First() is T))
        {
            return false;
        }
        foreach (var item in src)
        {
            if (des.IndexOf((T)item) > -1)
                return true;
        }
        return false;
    }
    public static bool IsNull(this object field)
    {
        return field == null;
    }
    public static bool IsValidMobile(this string field)
    {
        if (field.ToLower().Contains("09".ToLower()) && field?.Length==11)
        {
            return true;
        }
        return false;
    }
    public static bool HasValue(this object field)
    {
        if (field == null)
            return false;

        var type = field.GetType();

        if (type.Namespace == "System.Collections.Generic")
        {
            return ((IEnumerable<object>)field).Any();
        }
        if (type.IsArray)
        {
            return ((Array)field).Length > 0;
        }
        if (type == typeof(string))
        {
            return !string.IsNullOrEmpty(field.ToString());
        }
        if (type == typeof(DateTime))
        {
            return DateTime.Parse(field.ToString()) != DateTime.MinValue;
        }

        if (type.IsPrimitive && (
            type == typeof(int) || type == typeof(long) || type == typeof(short) ||
            type == typeof(byte) || type == typeof(float) || type == typeof(double) ||
            type == typeof(decimal)))
        {
            return Convert.ToDouble(field) != 0;
        }

        return true;
    }

}
