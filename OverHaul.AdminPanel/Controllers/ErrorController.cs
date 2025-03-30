using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using Overhaul.AdminPanel.Models;
using Microsoft.AspNetCore.Http;

namespace Overhaul.AdminPanel.Controllers;

public class ErrorController : Controller
{
    [Route("Error")]
    public IActionResult Index()
    {
        var feature = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (HttpContext.Request.Headers["Content-Type"] == "application/json")
        {
            var cuserr = new CustomError() { ErrorMessage = feature?.Error.Message, StatusCode = 201 };
            return new JsonResult(cuserr);
        }

        ViewBag.Error= feature?.Error.Message;
        return View();
    }

    
}
