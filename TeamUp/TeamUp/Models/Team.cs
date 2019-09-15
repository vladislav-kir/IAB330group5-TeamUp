using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class Team
    {
        public string name { get; set; }
        public string sport { get; set; }
        public string location { get; set; }
        public string bio { get; set; }
        public List<String> member { get; set; }
        public List<String> member_request { get; set; }
         public string team_leader { get; set; }
    }
}
