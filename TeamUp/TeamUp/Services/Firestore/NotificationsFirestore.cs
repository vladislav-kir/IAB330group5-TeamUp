using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;

namespace TeamUp.Services.Firestore
{
    public class NotificationsFirestore
    {
        /**
         * Get specific Notification based on notification_id
         * 
         * STRICTLY prohibited to unauthorised users, only fetch for my profile purpose
         */
        public static async Task<Notification> GetNotificationByIdAsync(string notification_id)
        {
            var document = await CrossCloudFirestore.Current
                                    .Instance
                                    .GetCollection("Notification")
                                    .GetDocument(notification_id)
                                    .GetDocumentAsync();

            var notification = document.ToObject<Notification>();

            return notification;
        }

        /**
         * Get all Notifications when given a List of Notification_ids  *Which defined in User collection*
         * 
         * STRICTLY prohibited to unauthorised users, only fetch for my profile purpose
         */
        public static async Task<List<Notification>> GetNotificationsAsync(List<string> notification_ids)
        {
            List<Notification> notificationsList = new List<Notification>();

            foreach(string id in notification_ids)
            {
                //Get each notification
                var notification = await GetNotificationByIdAsync(id);

                notificationsList.Add(notification);
            }

            return notificationsList;
        }

        public static async Task<List<Notification>> GetMyNotificationsAsync()
        {
            //Get my profile first
            var my_profile = await UsersFirestore.GetMyProfileAsync();

            //Then get all notifications based on list of notification_ids defined in my profile
            List<Notification> notifications = await GetNotificationsAsync(my_profile.notification_id);

            return notifications;
        }

        public static async Task UpdateNotificationAsync(Notification notification)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Notification")
                         .GetDocument(notification.Id)
                         .UpdateDataAsync(notification);
        }

        public static async Task CreateNotificationAsync(Notification notification)
        {
            //Check if this user is new ??
            await CrossCloudFirestore.Current
                        .Instance
                        .GetCollection("Notification")
                        .GetDocument(notification.Id)
                        .SetDataAsync(notification);
        }

        /**
         * Remove specific Notification
         */
        public static async Task DeleteNotificationAsync(string notification_id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Notification")
                         .GetDocument(notification_id)
                         .DeleteDocumentAsync();
        }

    }
}
