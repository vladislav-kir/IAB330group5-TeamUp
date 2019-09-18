using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EmailPasswordLogInPage : ContentPage
    {
        EmailPasswordLogInPageViewModel emailPasswordLogInPageViewModel;
        public EmailPasswordLogInPage(EmailPasswordLogInPageViewModel emailPasswordLogInPageViewModel)
        {
            InitializeComponent();
            BindingContext = this.emailPasswordLogInPageViewModel = emailPasswordLogInPageViewModel;
        }
    }
}