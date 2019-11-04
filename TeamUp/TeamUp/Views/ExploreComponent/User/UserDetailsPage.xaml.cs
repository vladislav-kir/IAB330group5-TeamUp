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
    public partial class UserDetailsPage : ContentPage
    {
        UserDetailsPageViewModel userDetailsPageViewModel;

        public UserDetailsPage(UserDetailsPageViewModel userDetailsPageViewModel)
        {
            
            InitializeComponent();
            this.ForceLayout();
            BindingContext = this.userDetailsPageViewModel = userDetailsPageViewModel;

            if(userDetailsPageViewModel.user.team == null)
                userDetailsPageViewModel.LoadTeamPlayingCommand.Execute(null);
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}