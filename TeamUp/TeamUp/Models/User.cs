using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class User
    {
        [Id]
        public string Id { get; set; }
        public int age { get; set; }
        public string bio { get; set; }
        public string email { get; set; }
        public List<String> invitation { get; set; }
        public Dictionary<string, string> level { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public Dictionary<string, string> role { get; set; }

        /**
         * This will map directly "team" from Firestore to --> List of string of UID that refer to team ID
         * **/
        [MapTo("team")]
        public List<String> team_uid { get; set; }

        /**
         * This will store the Team objects.
         * **/
        public List<Team> team { get; set; }
        public string avatar { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
