using MvvmHelpers;
using System;
using System.Collections.Generic;
using System.Text;
using TeamUp.Models;

namespace TeamUp.ViewModels
{
    public class TeamDetailsPageViewModel :BaseViewModel
    {
        public Team team { get; set; }
        public TeamDetailsPageViewModel(Team team = null)
        {
            Title = team?.name;
            this.team = team;
        }
    }
}
