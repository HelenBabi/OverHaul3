using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using overhaul.Domain.Entities;
using System.Text.Encodings.Web;
using System.Text;
using overhaul.Application.Models.User;
using Microsoft.AspNetCore.Authorization;
using overhaul.Application.Models;

namespace Overhaul.AdminPanel.Controllers
{

    public class AuthController : Controller
    {
        private readonly IUserStore<AppUser> _userStore;
        //private readonly IUserEmailStore<AppUser> _emailStore;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthController(
            IUserStore<AppUser> userStore,
            // IUserEmailStore<AppUser> emailStore,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager
            )
        {
            _userStore = userStore;
            //_emailStore = emailStore;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult CreateUser()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser(AppUserBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = new AppUser();
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Name = $"{model.FirstName} {model.LastName}";
            user.LoginCode = "000";
            await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
            //await _emailStore.SetEmailAsync(user, model.Email, CancellationToken.None);

            var result = await _userManager.CreateAsync(user, model.Password);
            return View(result);
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            string returnUrl = Url.Content("~/");
            var result = await _signInManager.PasswordSignInAsync(model.Mobile, model.Password, model.RememberMe, lockoutOnFailure: false);
            if (result.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            ModelState.AddModelError("CustomError", "اطلاعات کاربری صحیح نمی باشد");
            return View(model);

        }
        public async Task<IActionResult> LogOut()
        {
            var returnUrl = Url.Content("~/");
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");

            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return LocalRedirect(returnUrl);
            //return View();
        }
        [HttpPost]
        public async Task<IActionResult> LogOut(string returnUrl = "")
        {
            returnUrl = Url.Content("~/");
            await _signInManager.SignOutAsync();
            //_logger.LogInformation("User logged out.");

            // This needs to be a redirect so that the browser performs a new
            // request and the identity for the user gets updated.
            return LocalRedirect(returnUrl);

        }

    }
}
