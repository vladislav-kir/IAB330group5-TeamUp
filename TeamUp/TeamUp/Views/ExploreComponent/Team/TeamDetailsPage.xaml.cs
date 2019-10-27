using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Models;
using TeamUp.ViewModels;
using TeamUp.Services.Firestore;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetailsPage : ContentPage
    {
        TeamDetailsPageViewModel teamDetailsPageViewModel;
        bool userInTeam;
        public TeamDetailsPage(TeamDetailsPageViewModel teamDetailsPageViewModel)
        {
            InitializeComponent();
            this.teamDetailsPageViewModel = teamDetailsPageViewModel;
            BindingContext = this.teamDetailsPageViewModel;
            joinTeamButton.Text = "";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            userInTeam = await TeamsFirestore.IsUserInTeam(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
            if (!userInTeam)
            {
                joinTeamButton.Text = "Join Team";
            }
            else
            {
                joinTeamButton.Text = "Leave Team";
            }
        }

        async void JoinButtonClicked(object sender, EventArgs args)
        {
            if (!userInTeam)
            {
                await TeamsFirestore.AddUserToTeam(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
                joinTeamButton.Text = "Leave Team";
            }
            else
            {
                
                joinTeamButton.Text = "Join Team";
            }
        }
    }
}