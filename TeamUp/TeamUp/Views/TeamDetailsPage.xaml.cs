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
        TeamDetailsPageViewModel teamDetailsPageViewModel;
        public TeamDetailsPage(TeamDetailsPageViewModel teamDetailsPageViewModel)
        {
            InitializeComponent();
            BindingContext = this.teamDetailsPageViewModel = teamDetailsPageViewModel;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            
        }
    }
}