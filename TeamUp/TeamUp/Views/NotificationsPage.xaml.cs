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
    public partial class NotificationsPage : ContentPage
    {
        NotificationsPageViewModel notificationsPageViewModel;

        public NotificationsPage()
        {
            InitializeComponent();
            Title = "Notifications";

            BindingContext = notificationsPageViewModel = new NotificationsPageViewModel();

            if(notificationsPageViewModel.notificationsList.Count == 0)
            {
                notificationsPageViewModel.LoadNotificationsCommand.Execute(null);
            }
        }
    }
}