using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;
using Firebase.Storage;

namespace TeamUp.ViewModels
{
    public class UsersFirestore
    {
        static FirebaseStorage firebaseStorage = new FirebaseStorage("teamup-b7a43.appspot.com");

        public static async Task<String> GetUserAvatarURLAsync(User user)
        {
            return await firebaseStorage
                .Child("images")
                .Child("user")
                .Child(user.name)
                .Child(user.avatar)
                .GetDownloadUrlAsync();
        }

        /*
         Get Specific User based on its ID.
         Please go to Firestore database to look it up
         */
        public static async Task<User> GetUserByIdAsync(string id)
        {
            //Load document from Cloud Firestore
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(id)
                                        .GetDocumentAsync();

            // Convert Document to User Model
            var user = document.ToObject<User>();

            // Download avatar image
            user.avatar = await GetUserAvatarURLAsync(user);

            return user;
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

            // Download avatar image for each User in the UserList
            UsersList.ForEach(async user =>
            {
                user.avatar = await GetUserAvatarURLAsync(user);
            });

            return UsersList;
        }

        
    }
}
