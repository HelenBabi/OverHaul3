using iTextSharp.text.pdf;
using iTextSharp.text;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Contracts;
using Overhaul.Application;
using iTextSharp.text;

namespace Overhaul.Infrastructure.Tools;

public class PdfDataExporter : IDataExporter
{
    public  Task<string> ExportAsync<TData>(IEnumerable<TData> data, string FileName, string sheetName, string path)
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        DirectoryInfo outputDir = new DirectoryInfo(currentDirectory + "\\wwwroot" + path);
        if (!outputDir.Exists)
        {
            outputDir.Create();
        }

        string persianDate = DateTime.Now.GregorianToJalali().Replace("/", "-");
        string generatedFileName = $"{FileName}_{persianDate}_{Guid.NewGuid().ToString().Substring(0, 8)}.pdf";
        string fullPath = Path.Combine(outputDir.FullName, generatedFileName);

        using (FileStream fs = new FileStream(fullPath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            // ایجاد سند PDF
            Document pdfDoc = new Document(PageSize.A4, 25, 25, 30, 30);
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, fs);
            pdfDoc.Open();

            // بارگذاری فونت فارسی
            string fontPath = Path.Combine(currentDirectory, "wwwroot", "assets", "fonts", "XB-Zar.ttf");
            //string fontPath = Path.Combine(currentDirectory, "wwwroot", "assets", "fonts", "tahoma.ttf");
            BaseFont bf = BaseFont.CreateFont(fontPath, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
            iTextSharp.text.Font font = new iTextSharp.text.Font(bf, 10, iTextSharp.text.Font.NORMAL);


            // ایجاد جدول تک‌ستونه برای عنوان
            PdfPTable titleTable = new PdfPTable(1);
            titleTable.RunDirection = PdfWriter.RUN_DIRECTION_RTL;
            titleTable.WidthPercentage = 100;

            PdfPCell titleCell = new PdfPCell(new Phrase(sheetName, font));
            titleCell.HorizontalAlignment = Element.ALIGN_CENTER;
            //titleCell.Border = Rectangle.NO_BORDER; // بدون حاشیه
            titleCell.BorderWidth = 0;
            titleTable.AddCell(titleCell);
            pdfDoc.Add(titleTable); // اضافه کردن جدول عنوان به سند


            // ایجاد پاراگراف خالی برای فاصله
            Paragraph emptyLine = new Paragraph(" ");
            emptyLine.SpacingAfter = 3f;  // فاصله بعد از پاراگراف خالی (به واحد پیکسل)
            pdfDoc.Add(emptyLine);

            // ایجاد جدول
            var properties = typeof(TData).GetProperties();
            PdfPTable table = new PdfPTable(properties.Length);
            table.WidthPercentage = 100;
            table.RunDirection = PdfWriter.RUN_DIRECTION_RTL;  // تنظیم جهت راست به چپ برای کل جدول

            // اضافه کردن سرستون‌ها
            foreach (var property in properties)
            {
                var displayNameAttribute = property.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                string headerName = (displayNameAttribute == null) ? property.Name : displayNameAttribute.DisplayName;

                PdfPCell headerCell = new PdfPCell(new Phrase(headerName, font));
                headerCell.HorizontalAlignment = Element.ALIGN_CENTER;
                headerCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;  // تنظیم RTL برای هر سلول
                table.AddCell(headerCell);
            }

            // اضافه کردن داده‌ها به جدول
            foreach (var item in data)
            {
                foreach (var property in properties)
                {
                    var value = property.GetValue(item, null)?.ToString() ?? "";
                    PdfPCell dataCell = new PdfPCell(new Phrase(value, font));
                    dataCell.HorizontalAlignment = Element.ALIGN_CENTER;
                    dataCell.RunDirection = PdfWriter.RUN_DIRECTION_RTL;  // تنظیم RTL برای هر سلول
                    table.AddCell(dataCell);
                }
            }

            // اضافه کردن جدول به سند
            pdfDoc.Add(table);
            pdfDoc.Close();
        }

        string reportUrl =  generatedFileName;
        return Task.FromResult(reportUrl);
    }
}

