using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;

namespace Organization.App.Controllers
{
    [Route("Admin/[action]")]
    [Authorize(Roles = nameof(Roles.Admin))]
    public class CityController : Controller
    {

        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }


        [HttpGet]
        public IActionResult ListCity()
        {
            List<CityDto> CityDto = new List<CityDto>();
            CityDto = _cityRepository.GetAllQuery().Select(x => new CityDto
            {
                CityID = x.CityId,
                Name = x.Name,
            }).ToList();
            if (TempData["ModelErrorCat"] != null)
                ModelState.AddModelError("", TempData["ModelErrorCat"].ToString());

            return View(CityDto);
        }

        [HttpGet]
        public IActionResult AddCity()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCity(CityDto CityDto)
        {
            if (CityNameControl(CityDto.Name))
            {
                ModelState.AddModelError("", "There is a City with the same name");
                return View(CityDto);
            }
            else
            {
                _cityRepository.Add(new City { Name = CityDto.Name });
                return RedirectToAction(nameof(ListCity));
            }
            return View();
        }

        [HttpGet]
        public IActionResult RemoveCity(string? CityID)
        {

            int CityIDValue = 0;
            if (RouteValueControl(CityID, out CityIDValue))
            {
                if (!AnyUsingCityControl(CityIDValue))
                {
                    _cityRepository.Remove(_cityRepository.First(x => x.CityId.Equals(CityIDValue)));

                }
                else
                    TempData["ModelErrorCat"] = "The selected City cannot be deleted, it is using";
            }
            else
                TempData["ModelErrorCat"] = "There is a error , Please try again";
            return RedirectToAction(nameof(ListCity));
        }

        [HttpGet]
        public IActionResult UpdateCity(string CityID)
        {
            int CityIDValue = 0;
            if (RouteValueControl(CityID, out CityIDValue) && HasCityControl(CityIDValue))
            {

                CityDto CityDto = _cityRepository.GetWhere(x => x.CityId == CityIDValue).Select(y => new CityDto
                {
                    CityID = y.CityId,
                    Name = y.Name,
                }).First();
                return View(CityDto);
            }
            else
                TempData["ModelErrorCat"] = "There is a error , Please try again";
            return RedirectToAction(nameof(ListCity));
        }

        [HttpPost]
        public IActionResult UpdateCity(CityDto CityDto)
        {
            if (HasCityControl(CityDto.CityID.Value))
            {
                if (CityNameControl(CityDto.Name))
                {
                    ModelState.AddModelError("", "There is a City with the same name");
                    return View(CityDto);
                }
                else
                {
                    // City City = _cityRepository.First(x => x.CityID == CityDto.CityID.Value);
                    _cityRepository.Update(new City { Name = CityDto.Name, CityId = CityDto.CityID.Value });
                    return RedirectToAction(nameof(ListCity));
                }
            }
            else
            {
                ModelState.AddModelError("", "There is a error , Please try again.");
                return View(CityDto);
            }
        }

        bool RouteValueControl(string? value, out int ID)
        {
            if (value != null && int.TryParse(value, out ID))
            {
                return true;
            }
            else
            {
                ID = 0;
                return false;
            }
        }

        bool AnyUsingCityControl(int CityID)
        {
            bool result = (_cityRepository.Any(x => x.Activities.Any(y => y.UserActivities.Any(z => z.Activity.CityId.Equals(CityID)))) || !HasCityControl(CityID));
            return result;
        }
        bool HasCityControl(int CityID)
        {
            return _cityRepository.Any(x => x.CityId == CityID);

        }
        bool CityNameControl(string Name)
        {
            return _cityRepository.Any(x => x.Name.ToLower() == Name);
        }

    }
}