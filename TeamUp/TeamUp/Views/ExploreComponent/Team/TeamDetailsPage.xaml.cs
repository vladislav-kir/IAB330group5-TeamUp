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
            userStatus = await TeamsFirestore.GetRelationshipAsync(UsersFirestore.myProfile.Id, teamDetailsPageViewModel.Team);

            InfoFrame.IsVisible = true;
            InfoButton.IsEnabled = false;

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
                    var answer = await DisplayAlert("Exit", "Do you really want to leave your group ?", "Leave", "Cancel");
                    
                    if(answer)
                    {
                        //If is already inside Team, then button is for leaving the team
                        await TeamsFirestore.RemoveUserFromTeamAsync(UsersFirestore.myProfile.Id, teamDetailsPageViewModel.Team);
                        await UsersFirestore.RemoveTeamFromUserAsync(UsersFirestore.myProfile.Id, teamDetailsPageViewModel.Team);

                        //Remove notifications in database
                        await NotificationsFirestore.DeleteNotificationAsync(UsersFirestore.myProfile.Id + teamDetailsPageViewModel.Team.Id);

                        joinTeamButton.Text = "Join Team";
                        userStatus = (sbyte)relationshipType.isOutside;
                    }
                    
                    break;

                case (sbyte)relationshipType.isRequesting:
                    //If is requesting, then button is for cancelled
                    await TeamsFirestore.RemoveUserRequestFromTeamAsync(UsersFirestore.myProfile.Id, teamDetailsPageViewModel.Team);

                    //Remove arised notification
                    await NotificationsFirestore.DeleteNotificationAsync(UsersFirestore.myProfile.Id + teamDetailsPageViewModel.Team.Id);
                    await UsersFirestore.RemoveNotificationFromUserAsync(teamDetailsPageViewModel.Team.team_leader
                                                        , /*Generate ID = User_id + Team_id*/ UsersFirestore.myProfile.Id + teamDetailsPageViewModel.Team.Id);

                    joinTeamButton.Text = "Join Team";
                    userStatus = (sbyte)relationshipType.isOutside;
                    break;

                case (sbyte)relationshipType.isOutside:
                    //If is outside, then button is for request joining team
                    await TeamsFirestore.AddUserRequestToTeamAsync(UsersFirestore.myProfile.Id, teamDetailsPageViewModel.Team);

                    // Create a notification
                    Notification notification = new Notification
                    {
                        Id = UsersFirestore.myProfile.Id + teamDetailsPageViewModel.Team.Id,
                        team_id = teamDetailsPageViewModel.Team.Id,
                        user_id = UsersFirestore.myProfile.Id,
                        status = "",
                        type = "member_request"
                    };

                    // Arise a notification to the team leader of that team
                    await UsersFirestore.AddNotificationToUserAsync(teamDetailsPageViewModel.Team.team_leader, notification.Id);

                    //Push notification online
                    await NotificationsFirestore.CreateNotificationAsync(notification);

                    joinTeamButton.Text = "✔️ Requested";
                    joinTeamButton.BackgroundColor = Color.FromHex("#D3D3D3");
                    userStatus = (sbyte)relationshipType.isRequesting;
                    break;
            }

        }

        private void MembersButton_Clicked(object sender, EventArgs e)
        {
            InfoFrame.IsVisible = false;
            InfoButton.IsEnabled = true;
            MembersFrame.IsVisible = true;
            MembersButton.IsEnabled = false;
        }

        private void InfoButton_Clicked(object sender, EventArgs e)
        {
            InfoFrame.IsVisible = true;
            InfoButton.IsEnabled = false;
            MembersFrame.IsVisible = false;
            MembersButton.IsEnabled = true;
        }
    }
}