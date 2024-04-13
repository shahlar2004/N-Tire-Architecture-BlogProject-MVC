using Lesson.Entity.DTOs.Users;
using Lesson.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Lesson.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        public SignInManager<AppUser> signInManager { get; }

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(UserLoginDTO userLoginDTO)
        {
            if (userLoginDTO.Email == null || userLoginDTO.Password == null)
            {
                ModelState.AddModelError("", "E-mail adresiniz  və ya şifrəniz yanlışdır. ");
                return View();
            }
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByEmailAsync(userLoginDTO.Email);

                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(user, userLoginDTO.Password, userLoginDTO.RememberMe, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Home", new { Area = "Admin" });
                    }

                    else
                    {
                        ModelState.AddModelError("", "E-mail adresiniz  və ya şifrəniz yanlışdır. ");

                        return View();
                    }
                }

                else
                {
                    ModelState.AddModelError("", "E-mail adresiniz  və ya şifrəniz yanlışdır. ");
                    return View();
                }
            }

            else
            {
                return View();
            }
        }



        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth", new { Area = "Admin" });
        }


        [HttpGet]
        public async Task<IActionResult> AccessDenied()
        {
            return View();
        }
    }
}
