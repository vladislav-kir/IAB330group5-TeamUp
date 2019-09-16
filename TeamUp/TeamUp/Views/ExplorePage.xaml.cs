using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.ViewModels;
using Plugin.CloudFirestore;
using TeamUp.Models;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExplorePage : ContentPage
    {
        ExplorePageViewModel explorePageViewModel;
        
        public ExplorePage()
        {
            InitializeComponent();
            BindingContext = explorePageViewModel = new ExplorePageViewModel();
        }

        /* On first load, on appearing of the page. This will load all the users on the database */
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (explorePageViewModel.usersList.Count == 0)
                explorePageViewModel.LoadUsersCommand.Execute(null);

        }

        async void OnUserSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var user = args.SelectedItem as User;

            if (user == null)
                return;

            await Navigation.PushAsync(new UserDetailsPage(new UserDetailsPageViewModel(user)));
        }



    }
}