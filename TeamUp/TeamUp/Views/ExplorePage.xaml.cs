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
    public partial class ExplorePage : ContentPage
    {
        public ExplorePage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = "Explore";

            exploreUsersView.IsVisible = true;
            userButton.IsEnabled = false;
            exploreTeamsView.IsVisible = false;
            teamButton.IsEnabled = true;
        }

        private void User_Clicked(object sender, EventArgs e)
        {
            exploreUsersView.IsVisible = true;
            userButton.IsEnabled = false;
            exploreTeamsView.IsVisible = false;
            teamButton.IsEnabled = true;
        }

        private void Team_Clicked(object sender, EventArgs e)
        {
            exploreTeamsView.IsVisible = true;
            teamButton.IsEnabled = false;
            exploreUsersView.IsVisible = false;
            userButton.IsEnabled = true;
        }


    }
}