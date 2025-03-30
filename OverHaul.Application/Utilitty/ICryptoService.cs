using Overhaul.Application.AutoFac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Utilitty
{
    public interface ICryptoService : IScopedDependency
    {
        string Hash(string values);
        bool Check(string value, string hashedValue);
    }
}
