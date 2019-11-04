using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;

namespace XUnitTestProject1
{
    public class MyProfilePageTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function LoadMyProfile works correctly, return a profile of a user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_LoadMyProfile()
        {
            //Arrange 
            //Init the user
            string uid = "6H7I6z5BsDcyU1xP53DG2CAHOj53";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new ProfilePageViewModel();

            //Act
            vm.LoadMyProfile.Execute(null);

            //Assert
            Assert.True(vm.User.name.Equals("Fiora"));
        }


        /**
         * ------------------------
         * Test 2: Aims to test if function LoadMyProfile works correctly, return a profile of a user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_LoadMyProfile()
        {
            //Arrange 
            //Init the user
            string uid = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new ProfilePageViewModel();

            //Act
            vm.LoadMyProfile.Execute(null);

            //Assert
            Assert.True(vm.User.name.Equals("Volibear"));
        }


        /**
         * ------------------------
         * Test 3: Aims to test if function LoadMyProfile works correctly, return a profile of a user from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_LoadMyProfile()
        {
            //Arrange 
            //Init the user
            string uid = "mTwtRUKrzfamBp6PUEMGPMCRNBy1";
            var user = await UsersFirestore.GetUserByUIDAsync(uid);
            UsersFirestore.myProfile = user;

            var vm = new ProfilePageViewModel();

            //Act
            vm.LoadMyProfile.Execute(null);

            //Assert
            Assert.Contains("1fhFeGGSZsIyQqwPkuMA", vm.User.team_leader);
        }
    }
}
