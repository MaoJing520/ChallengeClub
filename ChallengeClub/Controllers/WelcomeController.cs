using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ChallengeClub.Repositories;

namespace ChallengeClub.Controllers
{
    public class WelcomeController : Controller
    {
        public readonly IConfiguration configuration;
        public readonly MemberRepository memberRepository;
        public WelcomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
            memberRepository = new MemberRepository(configuration);
        }

        [HttpGet("Welcome/{memberId}")]
        public IActionResult Welcome(int memberId)
        {
            memberRepository.CreateMemberLogin(memberId);

            var member = memberRepository.GetMemberById(memberId);
            return View(member);
        }
    }
}