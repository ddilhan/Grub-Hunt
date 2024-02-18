using Grub_Hunt.Web.DTOs;
using Grub_Hunt.Web.Interfaces;
using Grub_Hunt.Web.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Grub_Hunt.Web.Controllers
{
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpGet("SignIn")]
        public IActionResult SignIn()
        {
            return View(new SignInDTO());
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInDTO model)
        {
            if (ModelState.IsValid) 
            {
                var signInResult = await _authService.SignInAsync(model);

                if (signInResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    _authService.SignInUser(signInResult.Token);
                    return RedirectToAction("Index", "Home");
                }
                    
                else
                    ModelState.AddModelError("LoginError", signInResult.Message);
            }
            return View(model);
        }

        [HttpGet("SignUp")]
        public IActionResult SignUp()
        {
            var roleList = new List<SelectListItem>()
            {
               new SelectListItem(StaticDetails.RoleAdmin, StaticDetails.RoleAdmin),
               new SelectListItem(StaticDetails.RoleCustomer, StaticDetails.RoleCustomer)
            };

            ViewBag.RoleList = roleList;

            return View(new SignUpDTO());
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp(SignUpDTO model)
        {
            if (ModelState.IsValid)
            {
                var signUpResult = await _authService.SignUpAsync(model);
                if (signUpResult is not null && signUpResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var result = await _authService.AssignRoleAsync(model);
                    if (result is not null && result.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        TempData["success"] = "Registration Successful";
                        return RedirectToAction(nameof(SignIn));
                    }
                }
            }

            var roleList = new List<SelectListItem>()
            {
               new SelectListItem(StaticDetails.RoleAdmin, StaticDetails.RoleAdmin),
               new SelectListItem(StaticDetails.RoleCustomer, StaticDetails.RoleCustomer)
            };

            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpGet("SignOut")]
        public async Task<IActionResult> SignOut() 
        {
            await _authService.SignOutUser();
            return RedirectToAction("SignIn", "Auth");
        }
    }
}
