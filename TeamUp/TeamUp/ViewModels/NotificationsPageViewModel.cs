using MvvmHelpers;
using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    public class NotificationsPageViewModel : BaseViewModel
    {
        public ObservableCollection<Notification> notificationsList { get; set; }
        public Command LoadNotificationsCommand { get; set; }

        public NotificationsPageViewModel()
        {
            // Init the usersList
            notificationsList = new ObservableCollection<Notification>();
            LoadNotificationsCommand = new Command(async () => await ExecuteLoadNotificationsCommand());

            // Subscribe the snapshot
            updateNotificationsInRealTime();
        }

        async Task ExecuteLoadNotificationsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                notificationsList.Clear();
                var notifications = await NotificationsFirestore.GetMyNotificationsAsync();
                foreach (var notification in notifications)
                {
                    // Set the visibility of each notification
                    setVisibility(notification);

                    if(notification.isVisible)
                    {
                        // Load team & user 
                        notification.team = await TeamsFirestore.GetTeamByIdAsync(notification.team_id);
                        notification.user = await UsersFirestore.GetUserByUIDAsync(notification.user_id);

                        // Create a message
                        setNotificationContent(notification);

                        notificationsList.Add(notification);
                    }
                    

                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }

        private void setNotificationContent(Notification notification)
        {
            notification.isButtonVisible = false;
            /**
             * -----------------------
             * If this is a request to join the team
             * -----------------------
             * */
            if(notification.type.Equals("member_request"))
            {
                //If this user is team_leader & someone request to join your team && it has not been responded
                if (UsersFirestore.myProfile.team_leader.Contains(notification.team_id) && notification.status.Equals(""))
                {
                    //Set the message
                    notification.message = notification.user.name + " requests to join " + notification.team.name;

                    //Add button
                    notification.isButtonVisible = true;

                    //Set the avatar for the notification
                    notification.displayedAvatar = notification.user.avatar;
                }
                //If you are the request user && your request has been accepted
                else if (notification.user_id.Equals(UsersFirestore.myProfile.Id) && notification.status.Equals("accepted"))
                {
                    //Set the message
                    notification.message = notification.team.name + " accepted your request";


                    //Set the avatar for the notification
                    notification.displayedAvatar = notification.team.avatar;
                }
            }

            /**
             * -----------------------
             * If this is an invitation from a team
             * -----------------------
             * */
            else
            {
                //If this user is invited && status has not been responded
                if (notification.user_id.Equals(UsersFirestore.myProfile.Id) && notification.status.Equals(""))
                {
                    //Set the message
                    notification.message = notification.team.name + " invites you to join their team";

                    //Add button
                    notification.isButtonVisible = true;

                    //Set the avatar for the notification
                    notification.displayedAvatar = notification.team.avatar;
                }
                //If you are the request team --> you are the leader && your invitation has been accepted
                else if (UsersFirestore.myProfile.team_leader.Contains(notification.team_id) && notification.status.Equals("accepted"))
                {
                    //Set the message
                    notification.message = notification.user.name + " accepted to join your team " + notification.team.name;

                    //Set the avatar for the notification
                    notification.displayedAvatar = notification.user.avatar;
                }
            }
            


        }

        //Set the visibility of a notification based on some requirements
        private void setVisibility (Notification notification)
        {
            notification.isVisible = false;
            
            // Add some conditions when the notification should display or not

                
            if (
                //If it is a request from other user, and you are the team leader, and it has not been responded
                (notification.type.Equals("member_request") && UsersFirestore.myProfile.team_leader.Contains(notification.team_id)  && notification.status.Equals(""))

                //If it is your request, and it has been accepted
                || (notification.type.Equals("member_request") &&  notification.user_id.Equals(UsersFirestore.myProfile.Id) && notification.status.Equals("accepted"))

                //If it is an invitation from other team, and you are invited, and it has not been responded 
                || (notification.type.Equals("team_invitation") && notification.user_id.Equals(UsersFirestore.myProfile.Id) && notification.status.Equals(""))

                //If it is an invitation from your team, and you are team leader, and it has been accepted
                || (notification.type.Equals("team_invitation") && UsersFirestore.myProfile.team_leader.Contains(notification.team_id) && notification.status.Equals("accepted"))
                )
            {
                notification.isVisible = true;
            }
        }

        /*
        This function aims to update the Team --> ObservableList in realtime
        We add a snapshot, when there is an update, we will call ExecuteLoadTeamsCommand() that we have done above
             */
        public void updateNotificationsInRealTime()
        {
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("Notification")
                //This is the Realtime method, it happens whenever our data is changed (which is the user document)
                .AddSnapshotListener(async (snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        //Load the team again when changed
                        await ExecuteLoadNotificationsCommand();
                    }
                });
        }
    }
}
