using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChallengeClub.Models;
using System.Text;
using Microsoft.Extensions.Configuration;
using ChallengeClub.Repositories;

namespace ChallengeClub.Controllers
{
    [Route("MemberActivity")]
    public class MemberActivityController : Controller
    {
        public readonly IConfiguration configuration;
        public readonly ActivityRepository activityRepository;
        public MemberActivityController(IConfiguration configuration)
        {
            this.configuration = configuration;
            activityRepository = new ActivityRepository(configuration);
        }

        [HttpGet]
        public IActionResult MemberActivity()
        {

            IEnumerable<MemberActivity> memberActivity = activityRepository.GetActivities().Select(activity => {
                return new MemberActivity
                {
                    ActivityName = activity.Name,
                    ActivityId = activity.ActivityId,
                    ActivityImage = activity.ImagePath,
                    StartTime = activity.Hours,
                };

            });

            ActivityList ShowList = new ActivityList();

            ShowList.DailyActs = memberActivity.ToList();

            ShowList.SelectedActs = new List<MemberActivity> { }; 

            return View(ShowList);
        }

    

        [HttpPost]
        public IActionResult MemberActivity(ActivityList ls)
        {
            
            List<MemberActivity> TableList = new List<MemberActivity>();

             foreach (var item in ls.DailyActs) {
                 if (item.IsCheck) {
                    TableList.Add(item);
                 }
             }
             ActivityList ConfirmList = new ActivityList();
            
            ConfirmList.SelectedActs = TableList;
            ConfirmList.DailyActs = ls.DailyActs;

            return View(ConfirmList);
        }

        
    }
}