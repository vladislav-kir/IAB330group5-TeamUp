using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services;
using Xamarin.Forms;
using TeamUp.Services;

namespace TeamUp.ViewModels
{
    
    class FacebookLogInPageViewModel : BaseViewModel
    {
        private FacebookProfileModel facebookProfile;
        public FacebookProfileModel FacebookProfile
        {
            get { return facebookProfile; }
            set
            {
                facebookProfile = value;
                OnPropertyChanged();
            }
        }
        public async Task SetFacebookUserProfileAsync(string accessToken)
        {
            var facebookServices = new FacebookServices();

            FacebookProfile = await facebookServices.GetFacebookProfileAsync(accessToken);
        }

        public Command LogOutCommand
        {
            get
            {
                return new Command(() =>
                {
                    DependencyService.Get<IClearCookies>().ClearAllCookies();
                });
            }
        }
    }
}
