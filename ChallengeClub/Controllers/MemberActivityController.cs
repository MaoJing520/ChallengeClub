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
        public readonly MemberRepository memberRepository;
        public readonly ActivityRepository activityRepository;
        public readonly MemberActivityRepository memberActivityRepo;

        public MemberActivityController(IConfiguration configuration)
        {
            this.configuration = configuration;
            memberRepository = new MemberRepository(configuration);
            activityRepository = new ActivityRepository(configuration);
            memberActivityRepo = new MemberActivityRepository(configuration);
        }

        [HttpGet("{memberId}")]
        public IActionResult MemberActivity(int memberId)
        {

            IEnumerable<MemberActivity> memberActivity = activityRepository.GetActivities().Select(activity => {
                return new MemberActivity
                {

                    MemberId = memberId,

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
        public IActionResult PostMemberActivity([FromForm]ActivityList ls)
        {

            List<MemberActivity> TableList = new List<MemberActivity>();

            foreach (var item in ls.DailyActs)
            {
                if (item.IsCheck)
                {
                    TableList.Add(item);
                }
            }
            ActivityList ConfirmList = new ActivityList();

            ConfirmList.SelectedActs = TableList;
            ConfirmList.DailyActs = ls.DailyActs;


            return View("MemberActivity", ConfirmList);
        }

        [HttpPost("PostMemberActivity")]
        public IActionResult Confirm([FromForm]ActivityList activityList) {

            foreach(var act in activityList.SelectedActs)
            {
                memberActivityRepo.CreateMemberActivity(act.MemberId, act.ActivityId);
            }
        

            return View("Views/Logout/Logout.cshtml");
        }
    }
}