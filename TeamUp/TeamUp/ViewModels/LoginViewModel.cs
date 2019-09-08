using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using TeamUp.Views;

namespace TeamUp.ViewModels
{
    class LoginViewModel
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
