using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using ChallengeClub.Repositories;
using ChallengeClub.Models;

namespace ChallengeClub.Controllers
{
    [Route("Icon")]
    public class Icon_logoutController : Controller
    {

        public IActionResult Icon_logout()
        {
            return View();
        }
    }
}
