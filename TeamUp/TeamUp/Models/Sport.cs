using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class Sport
    {
        [Id]
        public string Id { get; set; }
        public string avatar { get; set; }
        public List<string> roles { get; set; }
        public string type { get; set; }
    }
}
