using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Views;
using Xamarin.Forms;
using System.Windows;
using TeamUp.Models;

namespace TeamUp.ViewModels
{
    public class LoginPageViewModel :BaseViewModel
    {
        

        public Command LoginCommand
        {
            get
            {
                return new Command( async () =>
                {
                    await App.Current.MainPage.Navigation.PushModalAsync(new FacebookLogInPage());
                });
            }
        }

        
    }
}
