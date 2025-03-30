using Overhaul.Application.AutoFac;
using Overhaul.Application.Utilitty;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Overhaul.Application.Services;

public interface ICurrentContextService : IScopedDependency
{
    bool IsAuthenticated();
    string GetCurrentUserId();
    Guid GetCurrentUserType();
    string GetCurrentUserAppLevelId();
    string GetCurrentUserFirstName();
    string GetCurrentUserLastName();
    string GetCurrentUserAvatar();
    string GetCurrentUserMobile();
    Claim GetClaim(string key);
    DateTime GetUserTokenExpiration();
    string GetUserToken();
    string GetCurrentUserName();
    string GetCurrentUserIp();
}
public class CurrentContextService : ICurrentContextService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IErrorFactory _errorFactory;

    public CurrentContextService(IHttpContextAccessor httpContextAccessor, IErrorFactory errorFactory)
    {
        _httpContextAccessor = httpContextAccessor;
        _errorFactory = errorFactory;
    }
    public string GetCurrentUserId()
    {
            var userIdClaim = GetCurrentUserIdClaim();
            if (userIdClaim == null)
            {
                throw _errorFactory.AccessDenied();
            }
            if (userIdClaim.Value != "")
                return userIdClaim.Value;
            throw _errorFactory.InvalidToken();

    }
    public string GetCurrentUserFirstName()
    {
        var userFirstNameClaim = GetCurrentUserFirstNameClaim();
        if (userFirstNameClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return userFirstNameClaim.Value;
    }
    public string GetCurrentUserAppLevelId()
    {
        var userTimezoneClaim = GetCurrentUserAppLevelIdClaim();
        if (userTimezoneClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return userTimezoneClaim.Value;
    }
    public Guid GetCurrentUserType()
    {
        var userTypeClaim = GetCurrentUserTypeClaim();
        if (userTypeClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }
        if (Guid.TryParse(userTypeClaim.Value, out Guid result))
            return result;
        throw _errorFactory.InvalidToken();
    }
    public string GetCurrentUserLastName()
    {
        var userLastNameClaim = GetCurrentUserLastNameClaim();
        if (userLastNameClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return userLastNameClaim.Value;
    }
    public string GetCurrentUserName()
    {
        var userUserNameClaim = GetCurrentUserUserNameClaim();
        if (userUserNameClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return userUserNameClaim.Value;
    }
    public string GetCurrentUserAvatar()
    {
        var imageClaim = GetAvatarClaim();
        if (imageClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return imageClaim.Value;
    }
    public string GetCurrentUserIp()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        return httpContext.Connection.RemoteIpAddress.ToString();
    }
    public string GetUserToken()
    {
        string token = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
        return token[7..];
    }
    public DateTime GetUserTokenExpiration()
    {
        var exp = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(x => x.Type == "exp");
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        return dtDateTime.AddSeconds(double.Parse(exp.Value));
    }
    public bool IsAuthenticated()
    {
        return GetCurrentUserIdClaim()?.Value != null;
    }
    private Claim GetCurrentUserIdClaim()
    {
        return GetClaim("UserId");
    }
    private Claim GetCurrentUserAppLevelIdClaim()
    {
        return GetClaim("AppLevelId");
    }
    private Claim GetCurrentUserFirstNameClaim()
    {
        return GetClaim("FirstName");
    }
    private Claim GetCurrentUserTypeClaim()
    {
        return GetClaim("UserTypeId");
    }
    private Claim GetCurrentUserLastNameClaim()
    {
        return GetClaim("LastName");
    }
    private Claim GetCurrentUserUserNameClaim()
    {
        return GetClaim("UserName");
    }
    private Claim GetAvatarClaim()
    {
        return GetClaim("Avatar");
    }
    private Claim GetMobileClaim()
    {
        return GetClaim("Mobile");
    }
    public Claim GetClaim(string key)
    {
        return _httpContextAccessor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == key);
    }

    public string GetCurrentUserMobile()
    {
        var mobileClaim = GetMobileClaim();
        if (mobileClaim == null)
        {
            throw _errorFactory.AccessDenied();
        }

        return mobileClaim.Value;
    }
}

public static class CallerInspector
{
    public static string GetCallingAssembly()
    {
        var stackTrace = new StackTrace();
        var frames = stackTrace.GetFrames();

        foreach (var frame in frames)
        {
            var method = frame.GetMethod();
            var assembly = method.DeclaringType?.Assembly;

            if (assembly != null)
            {
                return assembly.GetName().Name; // نام اسمبلی فراخوانی‌کننده
            }
        }

        return "Unknown";
    }

    public static bool IsApiCall()
    {
        var callingAssembly = GetCallingAssembly();
        return callingAssembly.Contains("Api"); // بررسی نام اسمبلی
    }

    public static bool IsMvcCall()
    {
        var callingAssembly = GetCallingAssembly();
        return callingAssembly.Contains("Mvc"); // بررسی نام اسمبلی
    }
}