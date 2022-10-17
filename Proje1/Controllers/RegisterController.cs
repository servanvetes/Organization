using Entities;
using Entities.Models;
using Entities.RepositoryClass;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;

namespace Proje1.Controllers
{
    [AllowAnonymous]
    [Route("[Action]")]
    public class RegisterController : Controller
    {
        private readonly IUserRepository _userRepository;
        public RegisterController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public IActionResult Register()
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
        public IActionResult Register(UserDto userDto)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Your data is missing or incorrect");
                return View(userDto);
            }
            else
            {
                if (_userRepository.Any(x => x.Email.ToLower().Equals(userDto.Email.ToLower())))
                {
                    ModelState.AddModelError(nameof(userDto.Email), "There is a user with this e-mail");
                    return View(userDto);
                }
                else
                {
                    User user = new User()
                    {
                        Email = userDto.Email,
                        Password = userDto.Password,
                        Surname = userDto.Surname,
                        Name = userDto.Name,
                        RoleId = userDto.RoleId,
                        CreatedDate = userDto.CreatedDate,

                    };
                    _userRepository.Add(user);
                    return RedirectToAction(nameof(LoginController.Login), "Login");
                }
            }


        }
    }
}
