using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using Overhaul.Application.Contracts;
using Overhaul.Application;
using OfficeOpenXml.Style;
using OfficeOpenXml;

namespace Overhaul.Infrastructure.Tools;

public class ExcellDataExporter : IDataExporter
{
    public Task<string> ExportAsync<TData>(IEnumerable<TData> data, string FileName, string sheetName, string path)
    {
        string reportUrl = string.Empty;
        string currentDirectory = Directory.GetCurrentDirectory();

        DirectoryInfo outputDir = new DirectoryInfo(currentDirectory + "\\wwwroot" + path);
        string generatedFileName = "";
        Random rnd = new Random();
        try
        {
            generatedFileName = $"{FileName}_{DateTime.Now.ToShortDateString().GetPersianDate().Replace("/", "-")}_{rnd.Next(111111, 999999)}.xlsx";
            FileInfo newFile = new FileInfo(Path.Combine(outputDir.FullName, generatedFileName));

            // بارگذاری داده‌ها به لیست
            List<TData> dataList = data.ToList();
            Int64 rowCount = dataList.Count;
            var fieldNames = typeof(TData).GetProperties().Select(f => f.Name).ToList();
            int columnCount = fieldNames.Count;


            string endColumnName = columnCount.GetExcelColumnName(); // پیدا کردن آخرین ستون اکسل


            using (ExcelPackage xlPackage = new ExcelPackage(newFile))
            {
                ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets.Add(sheetName);
                worksheet.PrinterSettings.LeftMargin = 0.3M;   
                worksheet.PrinterSettings.RightMargin = 0.3M;   

                // تنظیمات هدر اکسل
                using (ExcelRange r = worksheet.Cells["A1:" + endColumnName + "1"])
                {
                    System.Drawing.Font font = new System.Drawing.Font("Tahoma", 9);
                    r.Style.Font.SetFromFont(font);
                    r.Style.Font.Color.SetColor(Color.Black);
                    r.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.CenterContinuous;
                    r.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                    r.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(165, 213, 236));
                }

                // تنظیمات خط کشی بوردر اکسل
                var modelCells = worksheet.Cells["A1"];
                var modelRows = rowCount + 1;
                string modelRange = "A1:" + endColumnName + modelRows.ToString();
                var modelTable = worksheet.Cells[modelRange];
                modelTable.Style.Border.Top.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Left.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Right.Style = ExcelBorderStyle.Thin;
                modelTable.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                modelTable.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                modelTable.Style.VerticalAlignment = ExcelVerticalAlignment.Center;

                // نوشتن داده‌ها در اکسل
                modelCells.LoadFromCollection(dataList, true);

                modelTable.Style.Font.Size = 8; 
                modelTable.Style.Font.Name = "Tahoma"; 

                // نوشتن هدر جدول در اکسل
                worksheet.Row(1).Height = 20;
                // نوشتن هدر بر اساس DisplayAttribute.ShortName
                int columnIndex = 1;

                foreach (var property in typeof(TData).GetProperties())
                {
                    var displayNameAttribute = property.GetCustomAttribute(typeof(DisplayNameAttribute)) as DisplayNameAttribute;
                    if (displayNameAttribute != null)
                    {
                        worksheet.Cells[1, columnIndex].Value = displayNameAttribute.DisplayName;
                    }
                    else
                    {
                        worksheet.Cells[1, columnIndex].Value = property.Name;
                    }
                    columnIndex++;
                }

                modelTable.AutoFitColumns();
                xlPackage.Save();

                reportUrl =  generatedFileName;
                //string reportUrl = appClass.getWebConfigKeyValue("CRM_FILE") + "XLS/" + generatedFileName;
            }
        }
        catch (Exception ex)
        {
            string innerMessage = (ex.InnerException != null) ? ex.InnerException.Message : "";
        }
        return Task.FromResult(reportUrl);
    }
}