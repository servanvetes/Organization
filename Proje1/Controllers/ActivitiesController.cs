using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Proje1.DTOs;
using System.Security.Claims;

namespace Proje1.Controllers
{
    public class ActivitiesController : Controller
    {
        private IActivityRepository _activityRepository;
        public ActivitiesController(IActivityRepository activityRepository)
        {
            _activityRepository = activityRepository;
        }

        [HttpGet]
        [Authorize(Roles = nameof(Roles.User))]
        public IActionResult List(int? cityID, int? categoryID)
        {
            return View(ActivityTypes(ActivityStates.WithoutMyAct));
        }


        [HttpGet]
        [Authorize(Roles = nameof(Roles.User))]
        public IActionResult MyList()
        {

            return View(ActivityTypes(ActivityStates.MyAct));
        }

        [HttpGet]
        public IActionResult MyOldList()
        {

            return View(ActivityTypes(ActivityStates.MyOld));
        }

        [HttpGet]
        public IActionResult AllList()
        {

            return View(ActivityTypes(ActivityStates.AllAct));
        }


        private List<ActivitiesDto> ActivityTypes(ActivityStates activityStates)
        {
            List<ActivitiesDto> activities = new List<ActivitiesDto>();
            IQueryable<Activity>? ActivityQuery = null;
            int loginUserID = 0;
            switch (activityStates)
            {
                case ActivityStates.MyAct:

                    loginUserID = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    ActivityQuery = _activityRepository.GetWhere(x => x.ActivityId == (x.UserActivities.Where(a => a.UserId != loginUserID).Select(z => z.ActivityId).First()));
                    break;
                case ActivityStates.WithoutMyAct:
                    loginUserID = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value));
                    ActivityQuery = _activityRepository.GetWhere(x => x.ActivityId != (x.UserActivities.Where(a => a.UserId == loginUserID).Select(z => z.ActivityId).First()));
                    break;
                case ActivityStates.MyOld:
                    ActivityQuery = _activityRepository.GetAllQuery();
                    break;
                case ActivityStates.AllAct:
                    ActivityQuery = _activityRepository.GetAllQuery();
                    break;
                    //default:
                    //    favoriteTask = "Watching TV";
                    //    break;
            }


            //if (activityStates.Equals(ActivityStates.MyAct))
            //{
            //    int loginUserID = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value));
            //    ActivityQuery = _activityRepository.GetWhere(x => x.ActivityId == (x.UserActivities.Where(a => a.UserId != loginUserID).Select(z => z.ActivityId).First()));
            //}
            //else if (activityStates.Equals(ActivityStates.WithoutMyAct))
            //{
            //    int loginUserID = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value));
            //    ActivityQuery = _activityRepository.GetWhere(x => x.ActivityId != (x.UserActivities.Where(a => a.UserId == loginUserID).Select(z => z.ActivityId).First()));
            //}
            //else if (activityStates.Equals(ActivityStates.AllAct))
            //{
            //    ActivityQuery = _activityRepository.GetAllQuery();
            //}
            activities = ActivityQuery.Select(x => new ActivitiesDto
            {

                ActivityDate = DateOnly.FromDateTime(x.HappenedDate),
                Address = x.Address,
                ClosedDate = DateOnly.FromDateTime(x.ClosedDate),
                ActivityID = x.ActivityId,
                CityName = x.City.Name,
                Quota = x.Quota,
                CategoryName = x.Category.Name,
                Name = x.Name,
                Description = x.Description,
                IsTicked = (x.IsTicketed ? "Ticketed" : "Ticketless"),
            }).ToList();
            return activities;
        }
    }

    // MyOldList
    enum ActivityStates
    {
        WithoutMyAct = 0,
        MyAct = 1,
        MyOld = 2,
        AllAct = 3
    }
}
