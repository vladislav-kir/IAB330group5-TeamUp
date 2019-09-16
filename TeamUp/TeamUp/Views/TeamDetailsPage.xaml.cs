using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Models;
using TeamUp.ViewModels;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TeamDetailsPage : ContentPage
    {
        private Team team;
        private TeamsViewModel teamViewModel = new TeamsViewModel();

        public TeamDetailsPage(Team team)
        {
            InitializeComponent();
            this.team = team;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            nameView.Text = team.name;
            bioView.Text = team.bio;
        }
    }
}