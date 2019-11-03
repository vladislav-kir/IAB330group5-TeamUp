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
    class ExploreUsersViewViewModel : BaseViewModel
    {
        public ObservableCollection<User> usersList { get; set; }
        public Command LoadUsersCommand { get; set; }

        public ExploreUsersViewViewModel()
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

            // Loading .... --> The App become Busy
            IsBusy = true;

            try
            {
                usersList.Clear();
                var users = await UsersFirestore.GetAllUsersAsync();
                foreach (var user in users)
                {
                    // Exclude the my ID. Don't display myself in explore page
                    if (user.Id == UsersFirestore.myProfile.Id)
                        continue;

                    // Otherwise add other users into usersList
                    usersList.Add(user);

                    // Log the debug 
                    Debug.WriteLine("------------USER-------------");
                    Debug.WriteLine("**** name: " + user.name);
                    Debug.WriteLine("****  age: " + user.age);

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
