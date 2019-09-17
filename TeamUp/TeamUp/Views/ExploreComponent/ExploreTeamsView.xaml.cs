using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.ViewModels;
using TeamUp.Models;

namespace TeamUp.Views.ExploreComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExploreTeamsView : ContentView
    {
        ExploreTeamsViewViewModel exploreTeamsViewViewModel;
        public ExploreTeamsView()
        {
            InitializeComponent();
            BindingContext = exploreTeamsViewViewModel = new ExploreTeamsViewViewModel();

            if (exploreTeamsViewViewModel.teamsList.Count == 0)
                exploreTeamsViewViewModel.LoadTeamsCommand.Execute(null);

            
        }

        async void OnTeamSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var team = args.SelectedItem as Team;

            //Deselect item in ListView
            teamsListView.SelectedItem = null;

            if (team == null)
                return;

            await Navigation.PushAsync(new TeamDetailsPage(new TeamDetailsPageViewModel(team)));
        }
    }
}