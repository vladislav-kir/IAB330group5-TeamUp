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
        UserViewModel userViewModel = new UserViewModel();

        public ExplorePage()
        {
            InitializeComponent();
            Title = "Explore";

        }

        /* On first load, on appearing of the page. This will load all the users on the database */
        protected async override void OnAppearing()
        {
            base.OnAppearing();

            var usersList = await userViewModel.GetAllUsers();
            

            usersListView.ItemsSource = usersList;
        }

      
    }
}