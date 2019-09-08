using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : TabbedPage
    {
        public MenuPage()
        {
            InitializeComponent();
            this.Children.Add(new MyTeamsPage());
            this.Children.Add(new ExplorePage());
            this.Children.Add(new ProfilePage());
            this.Children.Add(new NotificationsPage());
            CurrentPage = this.Children[1];
        }
    }
}