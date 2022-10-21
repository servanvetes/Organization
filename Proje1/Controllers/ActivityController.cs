using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;
using System.Security.Claims;

namespace Proje1.Controllers
{

    [Authorize(Roles = nameof(Roles.User))]
    public class ActivityController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ICityRepository _cityRepository;
        private readonly ICategoryRepository _categoryRepository;
        public ActivityController(IActivityRepository activityRepository, ICityRepository cityRepository, ICategoryRepository categoryRepository)
        {
            _activityRepository = activityRepository;
            _cityRepository = cityRepository;
            _categoryRepository = categoryRepository;
        }
        [HttpGet]
        public IActionResult Add()
        {

            ActivityDto activityDto = new();

            activityDto.Cities = _cityRepository.GetAllQuery().Select(x => new CityDto { Name = x.Name, CityID = x.CityId }).ToList();
            activityDto.Categories = _categoryRepository.GetAllQuery().Select(x => new CategoryDto { Name = x.Name, CategoryID = x.CategoryId }).ToList();


            if (TempData["ModelErrorAddAct"] != null)
                ModelState.AddModelError("", TempData["ModelErrorAddAct"].ToString());

            return View(activityDto);
        }

        [HttpPost]
        public IActionResult Add(ActivityDto activityDto)
        {
            if (ModelState.IsValid)
            {
                Activity activity = new()
                {
                    Address = activityDto.Address,
                    CategoryId = activityDto.SelectedCategory.Value,
                    CityId = activityDto.SelectedCity.Value,
                    ClosedDate = activityDto.ClosedDate,
                    HappenedDate = activityDto.ActivityDate,
                    Description = activityDto.Description,
                    IsDeleted = false,
                    Name = activityDto.Name,
                    IsTicketed = activityDto.IsTicked,
                    Quota = activityDto.Quota,
                    Passive = false,
                    CreatUserId = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value)),
                    CreatedDate = DateTime.Now,
                };
                _activityRepository.Add(activity);
                return RedirectToAction(nameof(ActivitiesController.List), "Activities");
            }
            else
            {
                TempData["ModelErrorAddAct"] = "Please check dates and quota";
                return RedirectToAction(nameof(Add));
            }
        }
    }
}
