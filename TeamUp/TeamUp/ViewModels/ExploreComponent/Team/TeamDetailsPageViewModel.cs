using MvvmHelpers;
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
    public class TeamDetailsPageViewModel : BaseViewModel
    {
        public Team Team { get; set; }

        public bool isTeamLeader { get; set; }

        private User teamLeader;
        public User TeamLeader
        {
            get
            {
                return teamLeader;
            }
            set
            {
                teamLeader = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<User> Members { get; set; }

        public TeamDetailsPageViewModel(Team team = null)
        {
            Title = team?.name;
            Team = team;

            //Load leaders
            new Command(async () => { TeamLeader = await UsersFirestore.GetUserByUIDAsync(Team.team_leader); }).Execute(null);

            //Load members
            Members = new ObservableCollection<User>();
            new Command(async () => await ExecuteGetAllMembers()).Execute(null);

            // Whether this user is the team leader
            if (UsersFirestore.myProfile.team_leader.Contains(team.Id))
                isTeamLeader = true;
            else
                isTeamLeader = false;
        }

        public async Task ExecuteGetAllMembers()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                Members.Clear();
                foreach (string member_uid in Team.member)
                {
                    User member = await UsersFirestore.GetUserByUIDAsync(member_uid);

                    Members.Add(member);
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
    }
}
