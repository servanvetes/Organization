using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;
using System.Security.Claims;

namespace Proje1.Controllers
{
    [Route("[action]")]
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;

        public LoginController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginAsync(LoginDto loginDto)
        {
            User user = _userRepository.FirstOrDefault(x => x.Email.Equals(loginDto.Email) && x.Password.Equals(loginDto.Password));

            if (user == null)
            {
                ModelState.AddModelError("", "Please check your user information.");
                return View(loginDto);
            }
            else
            {
                List<Claim> claims = new();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.Name, $"{ user.Name } {user.Surname}"));
                claims.Add(new Claim(ClaimTypes.Role, user.RoleId==1 ? nameof(Roles.Admin): nameof(Roles.User)));

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal
                    , new AuthenticationProperties()
                    {
                        IsPersistent = loginDto.IsRememberMe,
                        ExpiresUtc = DateTime.UtcNow.AddMinutes(20)
                    });
                
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }


        public async Task<IActionResult> LogoutAsync()
        {

            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }
    }
}
