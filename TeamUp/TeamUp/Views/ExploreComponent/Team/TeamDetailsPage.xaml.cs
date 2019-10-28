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

        /**
         * Enum to represent the relationship between team & user
         */
        enum relationshipType
        {
            isOutside = 0,
            isRequesting = 1,
            isInside = 2
        }

        sbyte userStatus;

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

            //Establish relationship with team
            userStatus = await TeamsFirestore.relationship(UsersFirestore.userUID, teamDetailsPageViewModel.Team);

            switch(userStatus)
            {
                case (sbyte) relationshipType.isInside:
                    joinTeamButton.Text = "Leave Team";
                    break;
                case (sbyte)relationshipType.isRequesting:
                    joinTeamButton.Text = "✔️ Requested";
                    break;
                case (sbyte)relationshipType.isOutside:
                    joinTeamButton.Text = "Join Team";
                    break;
            }
            

        }

        async void JoinButtonClicked(object sender, EventArgs args)
        {
            switch (userStatus)
            {

                case (sbyte)relationshipType.isInside:
                    //If is already inside Team, then button is for leaving the team
                    await TeamsFirestore.RemoveUserFromTeam(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
                    await UsersFirestore.RemoveTeamFromUser(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
                    joinTeamButton.Text = "Join Team";
                    userStatus = (sbyte) relationshipType.isOutside;
                    break;

                case (sbyte)relationshipType.isRequesting:
                    //If is requesting, then button is for cancelled
                    await TeamsFirestore.RemoveUserRequestFromTeam(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
                    joinTeamButton.Text = "Join Team";
                    userStatus = (sbyte)relationshipType.isOutside;
                    break;

                case (sbyte)relationshipType.isOutside:
                    //If is outside, then button is for request joining team
                    await TeamsFirestore.AddUserRequestToTeam(UsersFirestore.userUID, teamDetailsPageViewModel.Team);
                    joinTeamButton.Text = "✔️ Requested";
                    joinTeamButton.BackgroundColor = Color.FromHex("#D3D3D3");
                    userStatus = (sbyte)relationshipType.isRequesting;
                    break;
            }

        }
    }
}