using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Overhaul.Application.Public;

public class SiteSetting
{
    public StringValues CacheControl { get; set; }
    public string AllowSpecificOrigins { get; set; }
    public string WithOrigins { get; set; }
    public string RSAKey { get; set; }
    public string AESKey { get; set; }
    public TokenSetting RoundTokenSetting { get; set; }
    public TokenSetting TokenSetting { get; set; }
    public TokenSetting AuthTokenSetting { get; set; }
    public int VerificationCodeExpireMinutes { get; set; }
    public string ClientSite { get; set; }
}