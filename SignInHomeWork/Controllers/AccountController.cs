using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SignInHomeWork.Models;
using SignInHomeWork.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignInHomeWork.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<TinyRick> rickManager;
        private readonly SignInManager<TinyRick> signInRick;

        public AccountController(UserManager<TinyRick> rickManager, SignInManager<TinyRick> signInRick)
        {
            this.rickManager = rickManager;
            this.signInRick = signInRick;
        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {

            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var rick = new TinyRick { UserName = vm.Email, Email = vm.Email };
            var result = await rickManager.CreateAsync(rick, vm.Password);
            if (ModelState.IsValid)
            {
                if (result.Succeeded)
                {
                    await signInRick.SignInAsync(rick, isPersistent: false);
                    return RedirectToAction("Index", "Home", rick);
                }
            }
            foreach (var err in result.Errors)
            {
                ModelState.AddModelError("", err.Description);
            }
            return View(vm);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            var result = await signInRick.PasswordSignInAsync(vm.Email, vm.Password, vm.RememberMe, false);
            if (ModelState.IsValid)
            {
                if (result.Succeeded)
                {
                    var user = await rickManager.FindByEmailAsync(vm.Email);
                    return RedirectToAction("Index", "Home", user);
                }
                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            }
            return View(vm);
        }

        [AcceptVerbs("Get", "Post")]
        [AllowAnonymous]
        public async Task<IActionResult> IsEmailUnique(string email)
        {
            var user = await rickManager.FindByEmailAsync(email);
            if (user is null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} Is Not Unique");
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await signInRick.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
    }
}
