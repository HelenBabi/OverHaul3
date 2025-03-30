using Overhaul.Application.Authentication;
using Overhaul.Application.AutoFac;
using Overhaul.Domain.Common;
using Overhaul.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Overhaul.Application.Public;

namespace Overhaul.Application.Utilitty;

public interface ITokenManagerService : IScopedDependency
{
    Task<bool> SetSecurityStamp(string userId, string securityStamp);
    Task<bool> IsActiveToken();
    Task<bool> CheckSecurityStamp(string userId, string hashValue);
    Task<bool> DeactivateToken(string token, DateTime expireDate);
    string GenerateSecurityStamp(AppUser user);
    Task<ClaimsPrincipal> ValidateToken(string token, TokenSetting tokenSetting);
    Task<string> GenerateToken(TokenSetting tokenSetting, params Claim[] claims);
}
