using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.ViewModels;
using TeamUp.Models;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyTeamsPage : ContentPage
    {

        MyTeamsPageViewModel myTeamsPageViewModel;
        public MyTeamsPage()
        {
            InitializeComponent();
            BindingContext = myTeamsPageViewModel = new MyTeamsPageViewModel();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (myTeamsPageViewModel.teamsList.Count == 0)
                myTeamsPageViewModel.LoadTeamsCommand.Execute(null);

        }

        async void OnTeamSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var team = args.SelectedItem as Team;

            if (team == null)
                return;

            await Navigation.PushAsync(new TeamDetailsPage(new TeamDetailsPageViewModel(team)));
        }
    }
}