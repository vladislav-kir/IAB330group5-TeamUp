using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Views;
using Xamarin.Forms;
using System.Windows;
using TeamUp.Models;
using TeamUp.Services.Firestore;

namespace TeamUp.ViewModels
{
    public class LoginPageViewModel :BaseViewModel
    {
        private string email;
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        public Command LoginFacebookCommand
        {
            get
            {
                return new Command( async () =>
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new FacebookLogInPage());
                });
            }
        }

        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        string uid = await AuthenticationFirebase.AuthenticateEmailPasswordAsync(Email, Password);

                        User user = await UsersFirestore.GetUserByUIDAsync(uid);
                        UsersFirestore.myProfile = user;

                        await App.Current.MainPage.Navigation.PushModalAsync(new EmailPasswordLogInPage(new EmailPasswordLogInPageViewModel(user)));
                    }
                    catch
                    {
                        await Application.Current.MainPage.DisplayAlert("Authentication Failed", "Wrong Email or Password", "Cancel");
                    }
                    
                });
            }
        }


    }
}
