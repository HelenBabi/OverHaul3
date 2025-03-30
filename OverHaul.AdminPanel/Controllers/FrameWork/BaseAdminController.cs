using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Overhaul.Application.Enums;
using Overhaul.Application.Public;

namespace Overhaul.AdminPanel.Controllers
{
    [Authorize]
    public class BaseAdminController : Controller
    {
        protected IActionResult HandleErrorResponse(JsonResponse res)
        {
            ViewBag.ErrorCode_ = res.ErrorCode;
            ViewBag.Message_ = res.Message;

            string errorViewPath = res.ErrorCode switch
            {
                MessageTitle.Not_Found => "/Views/Error/ErrorPartialGet.cshtml",
                _ => "/Views/Error/ErrorPartialGet.cshtml"
            };

            return Request.Headers["X-Requested-With"] == "XMLHttpRequest"
                ? PartialView(errorViewPath)
                : View(errorViewPath);
        }
    }
}
