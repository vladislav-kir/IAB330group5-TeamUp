using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;
using Firebase.Storage;

namespace TeamUp.Services.Firestore
{
    public class UsersFirestore
    {
        static FirebaseStorage firebaseStorage = new FirebaseStorage("teamup-b7a43.appspot.com");
        public static User myProfile { get; set; }
        
        public static async Task<String> GetUserAvatarURLAsync(User user)
        {
            //If the image is not a URL (it is stored on the cloud)
            if(!user.avatar.StartsWith("https://"))
                //Then fetch the image
                return await firebaseStorage
                    .Child("images")
                    .Child("user")
                    .Child(user.name)
                    .Child(user.avatar)
                    .GetDownloadUrlAsync();

            // Else return the original avatar URL
            return user.avatar;
        }

        /*
         Get Specific User based on its ID.
         Please go to Firestore database to look it up
         */
        public static async Task<User> GetUserByUIDAsync(string user_uid)
        {
            //Load document from Cloud Firestore
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(user_uid)
                                       
                                        .GetDocumentAsync();

            // Convert Document to User Model
            var user = document.ToObject<User>();

            // Download avatar image
            user.avatar = await GetUserAvatarURLAsync(user);


            return user;
        }

        /*
         Get My Profile
         Please go to Firestore database to look it up
         */
        public static async Task<User> GetMyProfileAsync()
        {
            return await GetUserByUIDAsync(UsersFirestore.myProfile.Id);
        }

        /*
         Get Specific User based on its Name.
         Please go to Firestore database to look it up
         */
        public static async Task<User> GetUserByNameAsync(string name)
        {
            //Load document from Cloud Firestore
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("User")
                                     .WhereEqualsTo("name", name)
                                     .GetDocumentsAsync();

            // Convert Document to User Model & get the First Result
            var user = query.ToObjects<User>().ToList().First();

            // Download avatar image
            user.avatar = await GetUserAvatarURLAsync(user);


            return user;
        }


        /*
         Get All of Users that are on the database
         */
        public static async Task<List<User>> GetAllUsersAsync()
        {
            
            // Load all documents from Cloud Firestore
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("User")
                                     .GetDocumentsAsync();

            // Convert to List of User Model
            var UsersList = query.ToObjects<User>().ToList();

            // Download avatar image & Team reference for each User in the UserList
            foreach(User user in UsersList)
            {
                user.avatar = await GetUserAvatarURLAsync(user);
            }

            return UsersList;
        }

        public static async Task<bool> IsNewUser(string userUID)
        {
            //Firstly Look up, whether there exists user
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(userUID)
                                        .GetDocumentAsync();
           
            return !document.Exists;
        }


        /*
         Add a new User to our Firestore Database
         */
        public static async Task AddUserAsync(User user)
        {
            
            //Check if this user is new ??
            if (await IsNewUser(user.Id))
                await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("User")
                         .GetDocument(user.Id)
                         .SetDataAsync(user);

        }

        /**
         * Adds a team to a user
         */
        public static async Task AddTeamToUserAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("User")
                         .GetDocument(userId)
                         .UpdateDataAsync("team", FieldValue.ArrayUnion(team.Id));
        }

        public static async Task RemoveTeamFromUserAsync(string userId, Team team)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("User")
                         .GetDocument(userId)
                         .UpdateDataAsync("team", FieldValue.ArrayRemove(team.Id));
        }

        /**
         * Remove user request from a team
         */
        public static async Task RemoveTeamInvitationFromUserAsync(string teamId, User user)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("Team")
                         .GetDocument(user.Id)
                         .UpdateDataAsync("invitation", FieldValue.ArrayRemove(teamId));
        }

        
        public static async Task AddNotificationToUserAsync(string userId, string notification_id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("User")
                         .GetDocument(userId)
                         .UpdateDataAsync("notifications", FieldValue.ArrayUnion(notification_id));
        }

        public static async Task RemoveNotificationFromUserAsync(string userId, string notification_id)
        {
            await CrossCloudFirestore.Current
                         .Instance
                         .GetCollection("User")
                         .GetDocument(userId)
                         .UpdateDataAsync("notifications", FieldValue.ArrayRemove(notification_id));
        }

    }
}
