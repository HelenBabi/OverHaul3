namespace WelFare.AdminPanel;

public interface ICookieService
{
    void WriteCookie(string key, string value);
    string ReadCookie(string key);
}

public class CookieService : ICookieService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CookieService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public void WriteCookie(string key, string value)
    {
        CookieOptions options = new CookieOptions
        {
            Expires = DateTime.Now.AddDays(1)
        };
        _httpContextAccessor.HttpContext.Response.Cookies.Append(key, value, options);
    }

    public string ReadCookie(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies[key];
    }
}
