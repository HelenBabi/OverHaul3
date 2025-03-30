using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WelFare.AdminPanel;

public class BaseController : Controller // از () به {} تغییر دهید
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    protected readonly IWebHostEnvironment _webHostEnvironment;

    public BaseController(IHttpContextAccessor httpContextAccessor, IWebHostEnvironment webHostEnvironment)
    {
        _httpContextAccessor = httpContextAccessor;
        _webHostEnvironment = webHostEnvironment;
    }

    public override void OnActionExecuting(ActionExecutingContext context)
    {
        ViewBag.RoleName = ReadCookie("RoleName");
        ViewBag.UserName = ReadCookie("UserNameFamily");
        ViewBag.AvatarPic = ReadCookie("AvatarPath");
        base.OnActionExecuting(context);
    }

    protected string ReadCookie(string key)
    {
        return _httpContextAccessor.HttpContext.Request.Cookies[key];
    }
}
//public override void OnActionExecuting(ActionExecutingContext context)
//{
//    ViewBag.RoleName = "ADMIN"; // مقداردهی ViewBag برای همه اکشن‌ها
//    base.OnActionExecuting(context);
//}

//protected string ReadCookie(string key)
//{
//    return _httpContextAccessor.HttpContext.Request.Cookies[key];
//}