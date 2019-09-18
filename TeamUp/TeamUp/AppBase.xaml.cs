using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Services;
using TeamUp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TeamUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppBase : Xamarin.Forms.Shell
    {
        public AppBase()
        {
            InitializeComponent();
            
        }

        
    }
}