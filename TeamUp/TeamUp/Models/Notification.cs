using Plugin.CloudFirestore.Attributes;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Services.Firestore;
using Xamarin.Forms;

namespace TeamUp.Models
{
    public class Notification
    {
        [Id]
        public string Id { get; set; }
        public string status { get; set; }
        public string team_id { get; set; }

        [Ignored]
        public Team team { get; set; }

        [Ignored]
        public User user { get; set; }

        [Ignored]
        public string message { get; set; }
        public string user_id { get; set; }
        public string type { get; set; }

        [Ignored]
        public bool isVisible { get; set; }

        // This field only for XAML, don't use for other purpose
        //
        // This avatar will be displayed whenever there is a request
        [Ignored]
        public string displayedAvatar { get; set; }

        [Ignored]
        public bool isButtonVisible { get; set; }

        [Ignored]
        public Command acceptCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Change the status
                    this.status = "accepted";

                    //Update the new notification to the database
                    await NotificationsFirestore.UpdateNotificationAsync(this);

                    if(this.type.Equals("member_request"))
                    {
                        //Remove request from team
                        await TeamsFirestore.RemoveUserRequestFromTeamAsync(user_id, team);

                        //Add member to team
                        await TeamsFirestore.AddUserToTeamAsync(user_id, team);

                        //Add team to user
                        await UsersFirestore.AddTeamToUserAsync(user_id, team);

                        //Notify requester or invite team
                        await UsersFirestore.AddNotificationToUserAsync(user_id, Id);
                    }
                    
                    else
                    //If this is the team_invitation
                    {
                        //Remove invitation from user
                        await UsersFirestore.RemoveTeamInvitationFromUserAsync(team_id, user);

                        //Add member to team
                        await TeamsFirestore.AddUserToTeamAsync(user_id, team);

                        //Add team to user
                        await UsersFirestore.AddTeamToUserAsync(user_id, team);
                    }
                    

                });
            }
        }

        [Ignored]
        public Command declineCommand
        {
            get
            {
                return new Command(async () =>
                {
                    //Change the status
                    this.status = "declined";

                    //Update the new notification to the database
                    await NotificationsFirestore.UpdateNotificationAsync(this);

                    //Remove request & invitation
                    if (this.type.Equals("member_request"))
                    {
                        //Remove request from team
                        await TeamsFirestore.RemoveUserRequestFromTeamAsync(user_id, team);
                    }
                    else
                    //If this is the team_invitation
                    {
                        //Remove invitation from user
                        await UsersFirestore.RemoveTeamInvitationFromUserAsync(team_id, user);
                    }
                });
            }
        }
    }
}
