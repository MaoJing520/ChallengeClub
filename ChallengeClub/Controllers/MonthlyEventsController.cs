using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChallengeClub.Models;
using Microsoft.Extensions.Configuration;
using ChallengeClub.Repositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ChallengeClub.Controllers
{
    [Route("MonthlyEvents")]
    public class MonthlyEventsController : Controller
    {

        public readonly IConfiguration configuration;
        public readonly MonthlyEventsRepository monthlyEventsRepository;
        public MonthlyEventsController(IConfiguration configuration)
        {
            this.configuration = configuration;
            monthlyEventsRepository = new MonthlyEventsRepository(configuration);
        }


        [HttpGet]
        public IActionResult MonthlyEvents()
        {
            var result = this.monthlyEventsRepository.GetMonthlyEvents();

            var model = new EventsModel
            {
                MonthlyEvents = result
            };
            return View("MonthlyEvents", model);
        }

        [HttpPost]
        public IActionResult MonthlyEvents([FromForm]string eventTitle, string eventDate, string eventDescription)
        {

            monthlyEventsRepository.AddMonthlyEvent(eventTitle, eventDate, eventDescription);

            ViewBag.Error = "The user is not exist.  Please Try Again.";
            return View();

        }

        [HttpPost("/deleteEvent")]
        public IActionResult MonthlyEvents([FromForm]int eventId)
        {

            monthlyEventsRepository.DeleteEventById(eventId);

            return RedirectToAction("MonthlyEvents");

        }
    }
}
