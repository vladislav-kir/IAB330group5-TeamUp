using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class FacebookProfile
    {
        public string Name { get; set; }
        public Picture Picture { get; set; }

        public string Email { get; set; }
    }

    public class Picture
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public string Url { get; set; }
    }

}
