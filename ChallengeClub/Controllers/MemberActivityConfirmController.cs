using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ChallengeClub.Models;
using ChallengeClub.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace ChallengeClub.Controllers
{
    public class MemberActivityConfirmController : Controller
    {
        private readonly IConfiguration configuration;
        private readonly ActivityRepository activityRepository;
        private readonly MemberRepository memberRepository;
        private readonly MemberActivityRepository memberActivityRepository;


        public MemberActivityConfirmController(IConfiguration configuration)
        {
            this.configuration = configuration;
            activityRepository = new ActivityRepository(configuration);
            memberRepository = new MemberRepository(configuration);
            memberActivityRepository = new MemberActivityRepository(configuration);
        }
        public ActionResult MemberActivityConfirm()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Members = this.memberRepository.GetMembers();
            ViewBag.Activities = this.activityRepository.GetActivities();
            ViewBag.MemberActivities = this.memberActivityRepository.GetMemberActivities();
            return View();
        }

        /*[HttpPost]
        public ActionResult Create(IFormCollection collection)
        {
            this.memberActivityRepository.CreateMemberActivity(
                collection["Member"],
                collection["Activity"]
            );
            return RedirectToAction("MemberActivityConfirm");
        }*/

        [HttpPost]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            this.memberActivityRepository.DeleteMemberActivity(id);
            return RedirectToAction("MemberActivityConfirm");
        }
    }
}