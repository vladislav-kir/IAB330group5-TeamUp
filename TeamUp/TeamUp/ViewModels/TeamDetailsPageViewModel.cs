using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Models;
using Xamarin.Forms;

namespace TeamUp.ViewModels
{
    public class TeamDetailsPageViewModel : BaseViewModel
    {
        public Team Team { get; set; }
        public TeamDetailsPageViewModel() { }
        public TeamDetailsPageViewModel(Team team = null)
        {
            Title = team?.name;
            Team = team;
        }

        
    }
}
