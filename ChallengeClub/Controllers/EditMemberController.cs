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
    [Route("EditMember")]
    public class EditMemberController : Controller
    {

        public readonly IConfiguration configuration;
        public readonly MemberRepository memberRepository;
        public EditMemberController(IConfiguration configuration)
        {
            this.configuration = configuration;
            memberRepository = new MemberRepository(configuration);
        }


        [HttpGet]
        public IActionResult EditMember()
        {
            var result = this.memberRepository.GetMembers();

            var model = new MemberModel
            {
                Member = result
            };
            return View("EditMember", model);
        }

        [HttpPost]
        public IActionResult EditMember([FromForm]string newMemberName, int newMemberNumber)
        {

            memberRepository.AddNewMember(newMemberName, newMemberNumber);

            return View();

        }

        [HttpPost ("/delete")]
        public IActionResult EditMember([FromForm]int memberId)
        {

            memberRepository.DeleteMemberById(memberId);

            return RedirectToAction("EditMember");

        }
    }
}
