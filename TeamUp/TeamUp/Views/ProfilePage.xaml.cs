using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        ProfilePageViewModel profilePageViewModel;

        public ProfilePage()
        {
            InitializeComponent();
            Title = "Profile";
            BindingContext = profilePageViewModel = new ProfilePageViewModel();

            if(profilePageViewModel.User == null)
            {
                profilePageViewModel.LoadMyProfile.Execute(null);
            }
        }
    }
}