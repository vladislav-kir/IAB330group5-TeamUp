using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Models;
using Xamarin.Forms;
using TeamUp.Services.Firestore;
using TeamUp.Views;

namespace TeamUp.ViewModels
{
    class CreateTeamViewModel : BaseViewModel
    {
        private Team team;
        private string name;
        private string sport;
        private string location;
        private string bio;
        public Team Team
        {
            get
            {
                return team;
            }

            set
            {
                team = value;
                OnPropertyChanged();
            }
        }
        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public string Sport
        {
            get
            {
                return sport;
            }

            set
            {
                sport = value;
                OnPropertyChanged();
            }
        }

        public string Location
        {
            get
            {
                return location;
            }

            set
            {
                location = value;
                OnPropertyChanged();
            }
        }

        public string Bio
        {
            get
            {
                return bio;
            }

            set
            {
                bio = value;
                OnPropertyChanged();
            }
        }

        public Command CreateTeamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    team = new Team
                    {
                        name = Name,
                        sport = Sport,
                        location = Location,
                        bio = Bio,
                        member = new List<string>() { UsersFirestore.userUID },
                        team_leader = UsersFirestore.userUID
                    };
                    await TeamsFirestore.AddTeamAsync(team);
                    await UsersFirestore.AddTeamToUser(UsersFirestore.userUID, team);
                    await App.Current.MainPage.Navigation.PushModalAsync(new TeamDetailsPage(new TeamDetailsPageViewModel(team)));
                });
            }
        }
    }
}
