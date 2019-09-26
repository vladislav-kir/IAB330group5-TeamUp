using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Services;
using TeamUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        /*
         This represents the State of the Explore Page, whether the view is User or Team

            If Team :   userView = false
            If User :   userView = true
        */
        public static bool userView;

        public ExplorePage()
        {
            InitializeComponent();

            // At first, set the userView is true. So it always show User View on the first load
            userView = true;

            // Set the view state based on User view
            setViewState();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Title = "Explore";



        }

        /**
         * When button User has been clicked
         */
        private void User_Clicked(object sender, EventArgs e)
        {
            userView = true;

            // Set the view state based on User view
            setViewState();
        }

        /**
         * When button Team has been clicked
         */
        private void Team_Clicked(object sender, EventArgs e)
        {
            userView = false;

            // Set the view state based on User view
            setViewState();
        }

        /**
         * This method is setting up the View for Explore Page
         * Including:
         *  + User Button is Enabled ?
         *  + User View is Visible ?
         *  + Team Button is Enabled ?
         *  + Team View is Visible ?
         */
        private void setViewState()
        {
            exploreUsersView.IsVisible = userView;
            userButton.IsEnabled = !userView;
            exploreTeamsView.IsVisible = !userView;
            teamButton.IsEnabled = userView;
        }

        private void LogOut_Clicked(object sender, EventArgs e)
        {
            
        }
    }
}