using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.AutoFac;

namespace Overhaul.Application.Contracts
{
    public interface ISmsProvider : ISingletonDependency
    {
        Task<bool> SendSms(string userName, string password, string number, string[] mobiles, string text);
    }
}
