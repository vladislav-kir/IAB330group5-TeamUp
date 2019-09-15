using Plugin.CloudFirestore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeamUp.Models;
using System.Linq;

namespace TeamUp.ViewModels
{
    class UserViewModel
    {
        
        public async Task<User> GetUser(string id)
        {
            var document = await CrossCloudFirestore.Current
                                        .Instance
                                        .GetCollection("User")
                                        .GetDocument(id)
                                        .GetDocumentAsync();

            var user = document.ToObject<User>();

            return user;
        }

        public async Task<List<User>> GetAllUsers()
        {
            var query = await CrossCloudFirestore.Current
                                     .Instance
                                     .GetCollection("User")
                                     .GetDocumentsAsync();

            var UserList = query.ToObjects<User>().ToList();

            foreach(User user in UserList)
            {
                Console.WriteLine("------------------------------------------");
                Console.WriteLine("name: " + user.name);
                Console.WriteLine("phone: " + user.phone);

            }
            return UserList;
        }
    }
}
