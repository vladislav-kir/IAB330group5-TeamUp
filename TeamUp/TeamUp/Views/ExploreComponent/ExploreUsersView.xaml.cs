using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views.ExploreComponent
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExploreUsersView : ContentView
    {
        ExploreUsersViewViewModel exploreViewViewModel;
        public ExploreUsersView()
        {
            InitializeComponent();
            BindingContext = exploreViewViewModel = new ExploreUsersViewViewModel();

            if (exploreViewViewModel.usersList.Count == 0)
                exploreViewViewModel.LoadUsersCommand.Execute(null);

            
        }

        async void OnUserSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var user = args.SelectedItem as User;

            //Deselect item in ListView
            usersListView.SelectedItem = null;

            if (user == null)
                return;

            await Navigation.PushAsync(new UserDetailsPage(new UserDetailsPageViewModel(user)));
        }
    }
}