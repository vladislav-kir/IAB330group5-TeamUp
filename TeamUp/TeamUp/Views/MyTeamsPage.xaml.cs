using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.ViewModels;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTeamsPage : ContentPage
    {

        TeamsViewModel teamsViewModel = new TeamsViewModel();
        public MyTeamsPage()
        {
            InitializeComponent();
            Title = "My Teams";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var teamsList = await teamsViewModel.GetAllTeams();


            myTeamsListView.ItemsSource = teamsList;
        }

        private async void TeamsListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var team = await teamsViewModel.GetTeamByName(e.SelectedItem.ToString());

            await Navigation.PushAsync(new TeamDetailsPage(team));
        }
    }
}