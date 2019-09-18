using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    public class EmailPasswordLogInPageViewModel
    {
        public User User{get; }
        public EmailPasswordLogInPageViewModel(User user)
        {
            User = user;

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
    }
}
