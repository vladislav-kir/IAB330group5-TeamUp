﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
namespace TeamUp.Services
{
    class FacebookServices
    {
        public async Task<FacebookProfileModel> GetFacebookProfileAsync(string accessToken)
        {
            var requestUrl =
                "https://graph.facebook.com/v2.7/me/?fields=name,picture,email&access_token="
                + accessToken;

            var httpClient = new HttpClient();

            var userJson = await httpClient.GetStringAsync(requestUrl);

            Console.WriteLine("-------------- JSON ------------");
            Console.WriteLine("--------------------" + userJson);
            var facebookProfile = JsonConvert.DeserializeObject<FacebookProfileModel>(userJson);

            return facebookProfile;
        }
    }
}
