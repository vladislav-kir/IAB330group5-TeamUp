using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TeamUp.Models;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UserDetailsPage : ContentPage
    {
        private User user;
        public UserDetailsPage(User user)
        {
            InitializeComponent();
            this.user = user;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            name.Text = user.name;
            phone.Text = user.phone;
            email.Text = user.email;
            age.Text = user.age.ToString();
            bio.Text = user.bio;
        }
    }
}