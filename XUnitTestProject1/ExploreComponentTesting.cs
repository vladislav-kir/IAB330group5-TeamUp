using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;


namespace XUnitTestProject1
{
    public class ExploreComponentTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function LoadTeamsCommand works correctly, return a list of team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_LoadMyProfile()
        {
            //Arrange 
            
            var vm = new ExploreTeamsViewViewModel();

            //Act
            vm.LoadTeamsCommand.Execute(null);

            //Assert
            Assert.True(vm.teamsList.Count > 1);
        }


        /**
         * ------------------------
         * Test 2: Aims to test if function LoadTeamsCommand works correctly, return a specific team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_LoadMyProfile()
        {

            //Arrange 
            string uid = "6H7I6z5BsDcyU1xP53DG2CAHOj53";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            string team_id = "Xu3FwHcyJpeDuXIk1vti";
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            var vm = new TeamDetailsPageViewModel(team);

            //Act
            

            //Assert
            Assert.True(vm.isTeamLeader);
        }


        /**
         * ------------------------
         * Test 3: Aims to test if function LoadTeamsCommand works correctly, return a specific team from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_LoadMyProfile()
        {

            //Arrange 
            string uid = "6H7I6z5BsDcyU1xP53DG2CAHOj53";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            string team_id = "sYvjfjfqarGNAptZ5myb";
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            var vm = new TeamDetailsPageViewModel(team);

            //Act


            //Assert
            Assert.True(vm.TeamLeader.Equals("HcHGkaJGpoOSNZ19hzqUjPqcoHj1"));
        }
    }
}
