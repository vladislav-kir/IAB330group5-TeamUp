using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Models;
using Xamarin.Forms;
namespace TeamUp.ViewModels
{
    public class UserDetailsPageViewModel : BaseViewModel
    {
        public User user { get; set; }

        public UserDetailsPageViewModel(User user = null)
        {
            Title = user?.name;
            this.user = user;
        }
    }
}
