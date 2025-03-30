using Overhaul.Application.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Utilitty
{
    public interface IErrorFactory : IScopedDependency
    {
        Exception InvalidToken();
        Exception AccessDenied();
        Exception ThisUserIsAltered();
        Exception FileNotFound();
        Exception XssDetect();
        Exception ApiProtectorAttackDetect();
    }
}
