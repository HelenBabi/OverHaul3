using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Utilitty
{
    public class ErrorFactory : IErrorFactory
    {
        public virtual Exception AccessDenied() { return new Exception("AccessDenied"); }
        public virtual Exception InvalidToken() { return new Exception("InvalidToken"); }
        public virtual Exception ThisUserIsAltered() { return new Exception("ThisUserIsAltered"); }
        public virtual Exception FileNotFound() { return new Exception("FileNotFound"); }
        public virtual Exception XssDetect() { return new Exception("XssDetect"); }
        public virtual Exception ApiProtectorAttackDetect() { return new Exception("ApiProtectorAttackDetect"); }
    }
}
