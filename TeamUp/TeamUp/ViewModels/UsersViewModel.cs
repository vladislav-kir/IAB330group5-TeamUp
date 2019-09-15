using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;

namespace TeamUp.ViewModels
{
    public class UserViewModel
    {
        
        /*
         Get Specific User based on its ID.
         Please go to Firestore database to look it up
         */
        public async Task<User> GetUserById(string id)
        {
            //Load document from Cloud Firestore
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(id)
                                        .GetDocumentAsync();

            // Convert Document to User Model
            var user = document.ToObject<User>();

            return user;
        }

        /*
         Get Specific User based on its Name.
         Please go to Firestore database to look it up
         */
        public async Task<User> GetUserByName(string name)
        {
            //Load document from Cloud Firestore
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("User")
                                     .WhereEqualsTo("name", name)
                                     .GetDocumentsAsync();

            // Convert Document to User Model & get the First Result
            var user = query.ToObjects<User>().ToList().First();

            return user;
        }


        /*
         Get All of Users that are on the database
         */
        public async Task<List<User>> GetAllUsers()
        {
            // Load all documents from Cloud Firestore
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("User")
                                     .GetDocumentsAsync();

            // Convert to List of User Model
            var UserList = query.ToObjects<User>().ToList();

            return UserList;
        }
    }
}
