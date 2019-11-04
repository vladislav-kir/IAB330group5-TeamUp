using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services;
using Xamarin.Forms;
using TeamUp.Services.Firestore;

namespace TeamUp.ViewModels
{
    
    class FacebookLogInPageViewModel : BaseViewModel
    {
        private User user;
        public User User
        {
            get { return user; }
            set
            {
                user = value;
                OnPropertyChanged();
            }
        }

        private FacebookProfile facebookProfile;
        public FacebookProfile FacebookProfile
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

        public Command GoToMainPage
        {
            get
            {
                return new Command(() =>
                {
                   App.Current.MainPage = new AppBase();
                });
            }
        }

        public void FirstUserInit(string uid)
        {
            User = new User { name = facebookProfile.Name, avatar = facebookProfile.Picture.Data.Url };

        }


        public Command AddUserToCloud
        {
            get
            {
                return new Command(async () =>
                {
                    UsersFirestore.myProfile = user;

                    await UsersFirestore.AddUserAsync(user);
                    App.Current.MainPage = new AppBase();
                });
            }
        }
        
           
        }
}
