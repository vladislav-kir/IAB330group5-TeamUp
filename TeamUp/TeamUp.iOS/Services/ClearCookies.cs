using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using TeamUp.iOS.Services;
using TeamUp.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClearCookies))]
namespace TeamUp.iOS.Services
{
    public class ClearCookies : IClearCookies
    {
        public void ClearAllCookies()
        {
            NSHttpCookieStorage CookieStorage = NSHttpCookieStorage.SharedStorage;
            foreach (var cookie in CookieStorage.Cookies)
                CookieStorage.DeleteCookie(cookie);

        }
    }
}