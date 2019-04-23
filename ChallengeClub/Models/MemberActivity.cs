using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChallengeClub.Models
{
    public class MemberActivity
    {
        public int Id { get; set; }
        public string Member { get; set; }
        public int MemberId { get; set; }
        public string Activity { get; set; }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; }
        public string StartTime { get; set; }
        public bool IsCheck { get; set; }
        public string ActivityImage { get; set; }
        public int MemberActivityId { get; set; }
    }


    public class ActivityList
    {

        public List<MemberActivity> DailyActs { get; set; }
        public List<MemberActivity> SelectedActs { get; set; }

    }
}
