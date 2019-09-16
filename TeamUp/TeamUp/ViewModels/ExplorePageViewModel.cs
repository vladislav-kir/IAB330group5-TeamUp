using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MvvmHelpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Models;
using System.Threading.Tasks;
using System.Diagnostics;
using TeamUp.Services.Firestore;

namespace TeamUp.ViewModels
{
    class ExplorePageViewModel : BaseViewModel
    {
        public ObservableCollection<User> usersList { get; set; }
        public Command LoadUsersCommand { get; set; }

        public ExplorePageViewModel()
        {
            Title = "Explore";

            // Init the usersList
            usersList = new ObservableCollection<User>();
            LoadUsersCommand = new Command(async () => await ExecuteLoadUsersCommand());
        }

        async Task ExecuteLoadUsersCommand()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                usersList.Clear();
                var users = await UsersFirestore.GetAllUsersAsync();
                foreach (var user in users)
                {
                    usersList.Add(user);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
