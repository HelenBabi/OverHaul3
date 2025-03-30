using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Enums
{
    public enum  LoginType
    {
        [Description(" رمز عبور")]
        Password=1,
        [Description("پیامک")]
        SMS=2
    }
    
}
