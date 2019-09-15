using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class User
    {
        public int age { get; set; }
        public string bio { get; set; }
        public string email { get; set; }
        public List<String> invitation { get; set; }
        public Dictionary<string, string> level { get; set; }
        public string name { get; set; }
        public string phone { get; set; }
        public Dictionary<string, string> role { get; set; }

        public List<String> team { get; set; }

        public override string ToString()
        {
            return this.name;
        }
    }
}
