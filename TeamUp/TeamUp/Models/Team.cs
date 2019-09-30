using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class Team
    {
        public string avatar { get; set; }
        [Id]
        public string Id { get; set; }
        public string name { get; set; }
        public string sport { get; set; }
        public string location { get; set; }
        public string bio { get; set; }
        public List<String> member { get; set; }
        public List<String> memberRequest { get; set; }
        public string teamLeader { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
