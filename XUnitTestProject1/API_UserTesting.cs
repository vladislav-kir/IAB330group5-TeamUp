using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;

namespace XUnitTestProject1
{
    public class API_UserTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function GetAllUsers works correctly, return a list of all users from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_GetAllUsers()
        {
            //Arrange 
            List<User> users = new List<User>();


            //Act
            users = await UsersFirestore.GetAllUsersAsync();

            //Assert
            Assert.True(users.Count > 1);
        }

        /**
         * ------------------------
         * Test 2: Aims to test if function GetUsersByUIDAsync works correctly, return a user
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_GetUsersByID()
        {
            //Arrange 
            string UID = "6H7I6z5BsDcyU1xP53DG2CAHOj53";

            //Act
            var user = await UsersFirestore.GetUserByUIDAsync(UID);

            //Assert
            Assert.True(user.name.Equals("Fiora"));
        }

        /**
         * ------------------------
         * Test 3: Aims to test if function GetUsersByUIDAsync works correctly, return a user
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_GetUsersByID()
        {
            //Arrange 
            string UID = "HcHGkaJGpoOSNZ19hzqUjPqcoHj1";

            //Act
            var user = await UsersFirestore.GetUserByUIDAsync(UID);

            //Assert
            Assert.True(user.phone.Equals("0452312423"));
        }

        /**
         * ------------------------
         * Test 4: Aims to test if function GetUsersByUIDAsync works correctly, return a user
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test4_GetUsersByID()
        {
            //Arrange 
            string UID = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";

            //Act
            var user = await UsersFirestore.GetUserByUIDAsync(UID);

            //Assert
            Assert.True(user.email.Equals("memphisto@email.com"));
        }

        /**
         * ------------------------
         * Test 5: Aims to test if function GetUserAvatarURLAsync works correctly, return a user's avatar downloadable link
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test5_GetUsersAvatar()
        {
            //Arrange 
            string UID = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";
            var user = await UsersFirestore.GetUserByUIDAsync(UID);

            //Act
            var avatar = await UsersFirestore.GetUserAvatarURLAsync(user);

            //Assert
            Assert.True(user.email.Equals("https://firebasestorage.googleapis.com/v0/b/teamup-b7a43.appspot.com/o/images%2Fuser%2FVolibear%2Fdownload.jpg?alt=media&token=2f3f1b00-e28c-4e08-81c2-b48ba072d185"));

        }


        /**
         * ------------------------
         * Test 5: Aims to test if function RemoveTeamFromUserAsync works correctly, which remove a specific team from user document
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test6_RemoveTeamFromUser()
        {
            //Arrange 
            string team_id = "1fhFeGGSZsIyQqwPkuMA";
            var team = await TeamsFirestore.GetTeamByIdAsync(team_id);

            string user_id = "6H7I6z5BsDcyU1xP53DG2CAHOj53";

            //Act
                //Remove the specific team from the user document
            await UsersFirestore.RemoveTeamFromUserAsync(user_id, team);

                //Fetch again the new document
            var user = await UsersFirestore.GetUserByUIDAsync(user_id);

            //Assert
            Assert.DoesNotContain(team_id, user.team_uid);
        }

        /**
         * ------------------------
         * Test 5: Aims to test if function RemoveNotificationFromUserAsync works correctly, which remove a specific notification from user document
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test7_RemoveNotificationFromUser()
        {
            //Arrange 
            string user_id = "mTwtRUKrzfamBp6PUEMGPMCRNBy1";
            string notification_id = "6xyKcZwxg1cXjUkjaggIf8WGWzu11fhFeGGSZsIyQqwPkuMA";

            //Act
            //Remove the specific notification from the user document
            await UsersFirestore.RemoveNotificationFromUserAsync(user_id, notification_id);

            //Fetch again the new document
            var user = await UsersFirestore.GetUserByUIDAsync(user_id);

            //Assert
            Assert.DoesNotContain(notification_id, user.notification_id);
        }
    }
}
