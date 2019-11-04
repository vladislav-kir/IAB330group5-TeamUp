using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;

namespace XUnitTestProject1
{
    public class MyTeamsPageTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function LoadTeamsCommand works correctly, return a list of team of user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_LoadTeamsCommand()
        {
            //Arrange 
                //Init the user
            string uid = "6H7I6z5BsDcyU1xP53DG2CAHOj53";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new MyTeamsPageViewModel();

            //Act
            vm.LoadTeamsCommand.Execute(null);

            //Assert
            Assert.True(vm.teamsList.Count > 1);
        }


        /**
         * ------------------------
         * Test 2: Aims to test if function LoadTeamsCommand works correctly, return a list of team of user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_LoadTeamsCommand()
        {
            //Arrange 
            //Init the user
            string uid = "HcHGkaJGpoOSNZ19hzqUjPqcoHj1";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new MyTeamsPageViewModel();

            //Act
            vm.LoadTeamsCommand.Execute(null);

            //Assert
            Assert.True(vm.teamsList.Count > 1);
        }

        /**
         * ------------------------
         * Test 3: Aims to test if function LoadTeamsCommand works correctly, return a list of team of user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_LoadTeamsCommand()
        {
            //Arrange 
            //Init the user
            string uid = "6xyKcZwxg1cXjUkjaggIf8WGWzu1";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new MyTeamsPageViewModel();

            //Act
            vm.LoadTeamsCommand.Execute(null);

            //Assert
            Assert.True(vm.teamsList[0].Id.Equals("1fhFeGGSZsIyQqwPkuMA"));
        }
    }
}
