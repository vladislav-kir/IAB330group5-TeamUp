using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TeamUp.Services;
using TeamUp.Views;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    public class ExploreViewModel : BaseViewModel
    {
        
        public Command LogOutCommand
        {
            get{
                return new Command( async () =>
                {
                    DependencyService.Get<IClearCookies>().ClearAllCookies();
                    await App.Current.MainPage.Navigation.PushModalAsync(new LoginPage());
                });
            }
        }
    }

    
}
