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
using TeamUp.Views;
using Plugin.CloudFirestore;

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

            // Subscribe the snapshot
            updateTeamInRealTime();
        }

        public Command AddTeamCommand
        {
            get
            {
                return new Command(async () => await App.Current.MainPage.Navigation.PushAsync(new CreateTeamPage()));
            }
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

        /*
        This function aims to update the Team --> ObservableList in realtime
        We add a snapshot, when there is an update, we will call ExecuteLoadTeamsCommand() that we have done above
             */
        public void updateTeamInRealTime()
        {
            CrossCloudFirestore.Current
                .Instance
                .GetCollection("User")
                .GetDocument(UsersFirestore.userUID)

                //This is the Realtime method, it happens whenever our data is changed (which is the user document)
                .AddSnapshotListener( async (snapshot, error) =>
                {
                    if (snapshot != null)
                    {
                        //Load the team again when changed
                        await ExecuteLoadTeamsCommand();
                    }
                });
        }
    }
}
