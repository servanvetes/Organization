using Entities;
using Entities.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proje1.DTOs;
using Proje1.Models;
using Proje1.ValidationClasses;
using System.Diagnostics;

namespace Proje1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController( /*ILogger<HomeController> logger*/)
        {

            //_logger = logger;
        }

        public IActionResult Index()
        {
            OrganizationContext context = new OrganizationContext();

            var degisken = context.Cities.ToList();
            HomeViewModel homeViewModel = new();
            homeViewModel.Sehirler = degisken.Select(x => new Sehir { Ad = x.Name, Id = x.CityId }).ToList();



            //  _logger.IsEnabled(LogLevel.Trace);
            return View(homeViewModel);
        }

        [HttpPost]
        public IActionResult Index(HomeViewModel homeViewModel)
        {
            var a = homeViewModel;
            return View(homeViewModel); 
        }

        [Route("duzenle/{id}")]
        public IActionResult duzenle(int? id)
        {
            
            return View();
        }

        [Authorize]
        public IActionResult Privacy()
        {
            if (User.Identity.IsAuthenticated)
            {

            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}