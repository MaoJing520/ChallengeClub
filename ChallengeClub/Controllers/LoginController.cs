using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChallengeClub.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace ChallengeClub.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        public readonly IConfiguration configuration;
        public readonly MemberRepository memberRepository;
        public readonly MemberLoginRepository memberLoginRepository;

        public LoginController(IConfiguration configuration)
        {
            this.configuration = configuration;
            memberRepository = new MemberRepository(configuration);
            memberLoginRepository = new MemberLoginRepository(configuration);
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index([FromForm]string memberId)
        {
            var isNumber = int.TryParse(memberId, out _);
            if (string.IsNullOrWhiteSpace(memberId) || memberId.Length != 4 || !isNumber)
            {
                ViewBag.Error = "The number must be 4-digits without space.  Please Try Again.";
                return View();

            }

            var member = memberRepository.GetMemberByNumber(memberId);
            int memId = member.MemberId;
            var loginMember = memberLoginRepository.GetAttendanceById(memId);
            if (member == null)
            {
                ViewBag.Error = "The number is not exist.  Please Try Again.";
                return View();
            }

            else
            {
                if (loginMember == null)
                {
                    return View("Views/Icon/Icon_login.cshtml", member);
                }

                else
                {
                    return View("Views/Icon/Icon_logout.cshtml", member);
                }

            }           
        }
    }
}