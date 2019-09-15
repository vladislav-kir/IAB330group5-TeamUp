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
        private User user;
        private UserViewModel userViewModel = new UserViewModel();

        public UserDetailsPage(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            avatarView.Source = user.avatar;
            nameView.Text = user.name;
            phoneView.Text = user.phone;
            emailView.Text = user.email;
            ageView.Text = user.age.ToString();
            bioView.Text = user.bio;
        }
    }
}