using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChallengeClub.Models;
using Microsoft.Extensions.Configuration;
using ChallengeClub.Repositories;

namespace ChallengeClub.Controllers
{
    [Route("EmployeeActivityManager")]
    public class EmployeeActivityManagerController : Controller
    {

        public readonly IConfiguration configuration;
        public readonly ActivityRepository activityRepository;
        public readonly ActivityDefinitionRepositories activityDefinitionRepository;

        public EmployeeActivityManagerController(IConfiguration configuration)
        {
            this.configuration = configuration;
            activityRepository = new ActivityRepository(configuration);
            activityDefinitionRepository = new ActivityDefinitionRepositories(configuration);
        }

        [HttpGet]
        public IActionResult EmployeeActivityManager()
        {
            var result = this.activityDefinitionRepository.GetActivityDefinition();

            var model = new ActivityManagerModel
            {
                EmployeeActivityDefinitions = result
            };

            return View("EmployeeActivityManager", model);
        }



        [HttpPost]
        public IActionResult EmployeeActivityManager([FromForm]string activityName, int activityHours, string activityDescription)
        {

            activityDefinitionRepository.AddActivityDefinition(activityName, activityHours, activityDescription);

            return View();

        }


        [HttpPost("activities")]
        public IActionResult EmployeeActivityAdd([FromForm]string submittedName, int submittedHours)
        {
            activityRepository.AddActivity(submittedName, submittedHours);
            return RedirectToAction("EmployeeActivityManager");
        }
    }
}

