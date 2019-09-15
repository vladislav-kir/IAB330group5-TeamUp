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

        TeamsViewModel MyTeamsViewModel = new TeamsViewModel();
        public MyTeamsPage()
        {
            InitializeComponent();
            Title = "My Teams";
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var usersList = await MyTeamsViewModel.GetAllTeams();


            myTeamsListView.ItemsSource = usersList;
        }
    }
}