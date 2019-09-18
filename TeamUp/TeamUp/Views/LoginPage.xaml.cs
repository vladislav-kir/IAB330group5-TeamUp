using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ViewModels;
using Xamarin.Forms;

namespace TeamUp.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class LoginPage : ContentPage
    {
        LoginPageViewModel loginPageViewModel;
        public LoginPage()
        {
            InitializeComponent();

            BindingContext = loginPageViewModel = new LoginPageViewModel();
        }


       
    }
}
