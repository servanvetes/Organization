using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;
using Proje1.Operations;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace Proje1.Controllers
{
    [Route("[Action]")]
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IUserRepository _userRepository;
        public ProfileController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Profile()
        {

            if (User.CheckMailNameId())
            {
                User? user = _userRepository.FirstOrDefault(x => x.Email.Equals(User.FindFirst(ClaimTypes.Email).Value) && x.UserId.Equals(Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value))));
                if (user != null)
                {
                    ProfileDto profileDto = new ProfileDto()
                    {
                        Email = user.Email,
                        Password = user.Password,
                        Name = user.Name,
                        Surname = user.Surname,
                    };
                    return View(profileDto);
                }
                else
                {
                    ModelState.AddModelError("", "There is a error.Please login again.");
                    return RedirectToAction(nameof(LoginController.LogoutAsync));
                }
            }
            else
            {
                ModelState.AddModelError("", "There is a error.Please login again.");
                return RedirectToAction(nameof(LoginController.LogoutAsync));
            }
        }

        [HttpPost]
        public IActionResult Profile(ProfileDto profileDto)
        {
            if (ModelState.IsValid || (User.CheckMailNameId()))
            {
                User user = _userRepository.FirstOrDefault(x => x.UserId.Equals(Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value))));
                user.Password = profileDto.Password;
                _userRepository.Update(user);
                return View(profileDto);
            }
            else
            {
                ModelState.AddModelError("", "There is a error.Please login again.");
                return RedirectToAction(nameof(LoginController.LogoutAsync));
            }
        }
    }
}
