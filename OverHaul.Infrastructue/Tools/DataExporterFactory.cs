using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Contracts;

namespace Overhaul.Infrastructure.Tools;

public class DataExporterFactory : IDataExporterFactory
{
    public IDataExporter Create(DataExporterTypes type)
    {
        if (type == DataExporterTypes.Excell)
            return new ExcellDataExporter();
        return new PdfDataExporter();

    }
}