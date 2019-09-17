using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Webkit;
using Android.Widget;
using TeamUp.Droid.Services;
using TeamUp.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(ClearCookies))]
namespace TeamUp.Droid.Services
{
    public class ClearCookies : IClearCookies
    {
        public void ClearAllCookies()
        {
            var cookieManager = CookieManager.Instance;
            cookieManager.RemoveAllCookie();
        }
    }
}