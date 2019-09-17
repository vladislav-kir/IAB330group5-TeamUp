using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Views;
using Xamarin.Forms;
using System.Windows;


namespace TeamUp.ViewModels
{
    class LoginPageViewModel :BaseViewModel
    {
        

        public Command LoginCommand
        {
            get
            {
                return new Command( () =>
                {
                    App.Current.MainPage = new FacebookLogInPage();
                });
            }
        }

        
    }
}
