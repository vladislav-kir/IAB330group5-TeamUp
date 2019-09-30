using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;
using TeamUp.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using TeamUp.Services.Firestore;


namespace TeamUp.ViewModels
{
    class MyTeamsPageViewModel: BaseViewModel
    {
        public ObservableCollection<Team> teamsList { get; set; }
        public Command LoadTeamsCommand { get; set; }

        public MyTeamsPageViewModel()
        {
            Title = "My Teams";

            // Init the usersList
            teamsList = new ObservableCollection<Team>();
            LoadTeamsCommand = new Command(async () => await ExecuteLoadTeamsCommand());
        }

        async Task ExecuteLoadTeamsCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                teamsList.Clear();
                var teams = await TeamsFirestore.GetMyTeamsAsync();
                foreach (var team in teams)
                {
                    teamsList.Add(team);
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
