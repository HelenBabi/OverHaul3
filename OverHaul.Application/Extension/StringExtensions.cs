using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Extensions;

public static class StringExtensions
{
    public static Guid ToGuid(this string value)
    {
        return Guid.TryParse(value, out var result) ? result : Guid.Empty;
    }

   public static string FileToRealExtension(this string FileHeaderHex)
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
            case "FF-D8-FF-E0":
                output = ".JPG";
                break;
            case "FF-D8-FF-E1":
                output = ".JPG";
                break;
            case "null":
                output = "file type is not matches with array";
                break;
        }
        return output;
    }
}