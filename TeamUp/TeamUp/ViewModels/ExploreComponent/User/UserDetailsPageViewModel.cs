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
    public class UserDetailsPageViewModel : BaseViewModel
    {
        public User user { get; set; }
        public ObservableCollection<Team> teamPlaying { get; set; }

        public Command LoadTeamPlayingCommand;
        public UserDetailsPageViewModel(User user = null)
        {
            Title = user?.name;
            this.user = user;
            teamPlaying = new ObservableCollection<Team>();

            LoadTeamPlayingCommand = new Command(async () => await LoadTeamPlaying());
        }

        private async Task LoadTeamPlaying ()
        {
            
               IsBusy = true;

               // Load Team
               try
               {
                teamPlaying.Clear();

                //For each ID reference from user --> Load into Team
                foreach(string team_uid in user.team_uid)
                {
                    //Load the team by its ID having in User
                    Team team = await TeamsFirestore.GetTeamByIdAsync(team_uid);

                    //Add it into collection of team
                    teamPlaying.Add(team);
                }
                
               }
               catch(Exception e)
               {
                   Debug.WriteLine(e);
               }
               finally
               {
                   IsBusy = false;
               }
          
        }
    }
}
