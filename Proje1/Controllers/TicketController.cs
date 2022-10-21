using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Organization.App.DTOs;
using Proje1.Controllers;
using Proje1.DTOs;
using System.Security.Claims;

namespace Organization.App.Controllers
{
    [Authorize]
    public class TicketController : Controller
    {
        private readonly IActivityRepository _activityRepository;
        private readonly ITicketCompanyRepository _ticketCompanyRepository;
        private readonly IUserActivityRepository _userActivityRepository;
        public TicketController(IActivityRepository activityRepository, ITicketCompanyRepository ticketCompanyRepository, IUserActivityRepository userActivityRepository)
        {
            _activityRepository = activityRepository;
            _ticketCompanyRepository = ticketCompanyRepository;
            _userActivityRepository = userActivityRepository;
        }


        [HttpGet]
        public IActionResult Buy(TicketActivityDto ticketActivityDto)
        {
            return View(ticketActivityDto);
        }


        [HttpPost]
        public IActionResult CompanyProcessing(string ActivityID, string SelectedCompany)
        {

            _userActivityRepository.Add(new UserActivity { ActivityId = Convert.ToInt32(ActivityID), CompanyId = Convert.ToInt32(SelectedCompany), UserId = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value)) });

            return RedirectToAction(nameof(ActivitiesController.MyList), "Activities");
        }


        [HttpPost]
        public IActionResult Buy(string ActivityID)
        {
            //if (ModelState.IsValid)
            try
            {
                int ActivityIDValue = 0;
                if (int.TryParse(ActivityID, out ActivityIDValue))
                {

                    TicketActivityDto ticketActivityDto = _activityRepository.GetWhere(x => x.ActivityId.Equals(ActivityIDValue)).
                        Select(y => new TicketActivityDto
                        {
                            ActivityID = y.ActivityId,
                            ActivityName = y.Name,
                            UserID = Convert.ToInt32((User.FindFirst(ClaimTypes.NameIdentifier).Value)),
                            TicketCompanies = _ticketCompanyRepository.GetAllQuery().Select(c => new TicketCompanyDto
                            {
                                CompanyId = c.CompanyId,
                                Name = c.Name,
                                Token = c.Token
                            }).ToList()
                        }).First();
                    return View(ticketActivityDto);
                }
                else
                    return RedirectToAction(nameof(ActivitiesController.List), nameof(ActivitiesController));
            }
            catch (Exception ex)
            {
                return RedirectToAction(nameof(ActivitiesController.List), nameof(ActivitiesController));
            }

            //return View();
        }


    }
}
