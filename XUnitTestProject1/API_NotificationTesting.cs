using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using TeamUp.ViewModels;
using Xunit;

namespace XUnitTestProject1
{
    public class API_NotificationTesting
    {
        /**
         * ------------------------
         * Test 1: Aims to test if function GetNotificationByIdAsync works correctly, return a notification from the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test1_GetNotificationByIdAsync()
        {
            //Arrange 
            string notification_id = "6xyKcZwxg1cXjUkjaggIf8WGWzu11fhFeGGSZsIyQqwPkuMA";
            Notification notification;

            //Act
            notification = await NotificationsFirestore.GetNotificationByIdAsync(notification_id);

            //Assert
            Assert.True(notification.type.Equals("member_request"));
        }


        /**
         * ------------------------
         * Test 2: Aims to test if function CreateNotificationAsync works correctly, create a notification and push to the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test2_CreateNotificationAsync()
        {
            string user_id = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";
            string team_id = "1fhFeGGSZsIyQqwPkuMA";
            //Arrange 
            Notification notification = new Notification { 
                team_id= team_id,
                user_id= user_id,
                status = "",
                type = "team_invitation"
            };

            //Act
            await NotificationsFirestore.CreateNotificationAsync(notification);

                //Notification ID  =  User ID  + Team ID   --> To create 1-1 mapping, avoid users to request multiple times
            var newNotification = await NotificationsFirestore.GetNotificationByIdAsync(user_id + team_id);

            //Assert
            Assert.True(newNotification.type.Equals("team_invitation"));
        }


        /**
         * ------------------------
         * Test 3: Aims to test if function DeleteNotificationAsync works correctly, delete a notification and push to the Cloud Firestore
         * ------------------------
         * 
         * **/
        [Fact]
        public async Task Test3_DeleteNotificationAsync()
        {
            string user_id = "ryZJ4v8INRes4o2ZxEaNOPDtqgo2";
            string team_id = "1fhFeGGSZsIyQqwPkuMA";

            //Act
            //Notification ID  =  User ID  + Team ID   --> To create 1-1 mapping, avoid users to request multiple times
            await NotificationsFirestore.DeleteNotificationAsync(user_id + team_id);

            var notification = await NotificationsFirestore.GetNotificationByIdAsync(user_id + team_id);

            //Assert
            Assert.Null(notification);
        }
    }
}
