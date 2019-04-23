using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeClub.Models
{
    public class Activity
    {
        public int ActivityId { get; set; }
        public string Name { get; set; }
        public string Hours { get; set; }
        public string ImagePath { get; set; }
        public string Description { get; set; }
    }
}
