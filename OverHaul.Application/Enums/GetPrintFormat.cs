using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Enums;

public enum  GetPrintFormat
{
    [Description("application/pdf")]
    PDF ,
    [Description("application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
    DOCX ,
    [Description("image/Png")]
    PNG
}

