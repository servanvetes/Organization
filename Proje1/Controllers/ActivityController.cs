using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;
using System.Security.Claims;

namespace Proje1.Controllers
{

    [Authorize(Roles = nameof(Roles.User))]
    public class ActivityController : Controller
    {


        [HttpGet]
        public IActionResult Add()
        {

            OrganizationContext context = new OrganizationContext();


            ActivityDto activityDto = new();
            activityDto.Cities = context.Cities.Select(x => new CityDto { Name = x.Name, CityID = x.CityId }).ToList();
            activityDto.Categories = context.Categories.Select(x => new CategoryDto { Name = x.Name, CategoryID = x.CategoryId }).ToList();

            return View(activityDto);
        }

        [HttpPost]
        public IActionResult Add(ActivityDto activityDto)
        {
            OrganizationContext context = new OrganizationContext();

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
            context.Activities.Add(activity);
            context.SaveChanges();

            // Etkinlik sayfasına yönlendirilecek
            //return View();
            return RedirectToAction(nameof(HomeController.Index));
        }
    }
}
