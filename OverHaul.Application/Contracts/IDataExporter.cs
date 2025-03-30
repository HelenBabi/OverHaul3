using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Contracts;

public interface  IDataExporter
{
    Task<string> ExportAsync<TData>(IEnumerable<TData> data  , string FileName, string sheetName , string path);

}



public interface IDataExporterFactory
{
    IDataExporter Create(DataExporterTypes type);
}

public enum DataExporterTypes : byte
{
    Excell,Pdf
}