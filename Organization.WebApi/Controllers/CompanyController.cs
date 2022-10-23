using Entities.Models;
using Entities.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Organization.WebApi.DTOs;
using Organization.WebApi.Services;

namespace Organization.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]

    public class CompanyController : ControllerBase
    {
        private IConfiguration _configuration;
        private readonly ITicketCompanyRepository _ticketCompanyRepository;
        private readonly IActivityRepository _activityRepository;

        public CompanyController(ITicketCompanyRepository ticketCompanyRepository, IActivityRepository activityRepository, IConfiguration configuration)
        {
            _ticketCompanyRepository = ticketCompanyRepository;
            _activityRepository = activityRepository;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult AddCompany(AddCompanyDto AddcompanyDto)
        {

            // var identity = HttpContext.User.Identity as ClaimsIdentity;
            try
            {
                if (HasEmailControl(AddcompanyDto.Email))
                {
                    return StatusCode(501, "There is a company with this Email");
                }
                else
                {
                    TokenService tokenService = new(_configuration);
                    string TokenValue = tokenService.CreateCompanyToken(AddcompanyDto);

                    // Refresh Token Değeri Db'ye kaydedilebilir

                    _ticketCompanyRepository.Add(new TicketCompany { DomainName = AddcompanyDto.DomainName, Email = AddcompanyDto.Email, Password = AddcompanyDto.Password, Name = AddcompanyDto.Name, Token = TokenValue });

                    return Ok(new CompanyDto() { DomainName = AddcompanyDto.DomainName, Email = AddcompanyDto.Email, Password = AddcompanyDto.Password, Name = AddcompanyDto.Name, Token = TokenValue });
                }

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetActivitiesList()
        {
            try
            {
                List<ActivitiesDto> activitiesDtos = new List<ActivitiesDto>();
                activitiesDtos = _activityRepository.GetAllQuery().Select(act => new ActivitiesDto
                {
                    ActivityDate = act.HappenedDate.ToString("dd/MM/yyyy"),
                    ActivityID = act.ActivityId,
                    Address = act.Address,
                    CategoryName = act.Category.Name,
                    CityName = act.City.Name,
                    Description = act.Description,
                    IsTicked = act.IsTicketed,
                    Name = act.Name,
                    Quota = act.Quota,
                    ClosedDate = act.ClosedDate.ToString("dd/MM/yyyy")
                }).ToList();

                if (activitiesDtos.Count == 0)
                    return NoContent();
                else
                    return Ok(activitiesDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [NonAction]
        private bool HasEmailControl(string Email)
        {

            return _ticketCompanyRepository.Any(x => x.Email.ToLower() == Email.ToLower());
        }
    }
}
