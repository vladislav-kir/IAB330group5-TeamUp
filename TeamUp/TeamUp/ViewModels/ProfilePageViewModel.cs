﻿using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    public class ProfilePageViewModel : BaseViewModel
    {
        private User user;
        public User User
        {
            get
            {
                return user;
            }
            set
            {
                SetProperty(ref user, value);
                OnPropertyChanged();
            }
        }

        public Command LoadMyProfile;

        public ProfilePageViewModel()
        {
            LoadMyProfile = new Command(async () => await loadMyProfileAsync());

        }

        private async Task loadMyProfileAsync()
        {
            IsBusy = true;
            try
            {
                User = await UsersFirestore.GetMyProfileAsync();
                IsBusy = false;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e);
            }
        }
    }
}