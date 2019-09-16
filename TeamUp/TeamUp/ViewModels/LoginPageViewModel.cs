using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Views;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    class LoginPageViewModel
    {
        public Command LoginCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MenuPage());
                });
            }
        }
    }
}
