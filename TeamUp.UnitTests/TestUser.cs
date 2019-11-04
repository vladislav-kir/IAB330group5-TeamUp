using System;
using System.Threading.Tasks;
using Xunit;
using TeamUp.Models;
using TeamUp.Services.Firestore;
using System.Collections.Generic;

namespace TeamUp.UnitTests
{
    public class TestUser
    {
        string user_uid = "6H7I6z5BsDcyU1xP53DG2CAHOj53";
        string name = "Fiora";

        [Fact]
        public async Task TestGetUserByUIDAsync()
        {
            User user = await UsersFirestore.GetUserByUIDAsync(user_uid);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task TestGetUserByNameAsync()
        {
            User user = await UsersFirestore.GetUserByNameAsync(name);
            Assert.NotNull(user);
        }

        [Fact]
        public async Task TestGetAllUsersAsync()
        {
            List<User> users = await UsersFirestore.GetAllUsersAsync();
            Assert.NotNull(users);
        }

        [Fact]
        public async Task TestIsNewUser()
        {
            string id = "D5BscyU1x6H3P53DG2CA7I6zHOj5";
            bool isNew = await UsersFirestore.IsNewUser(id);
            Assert.True(isNew);
        }

        [Fact]
        public async Task TestIsNotNewUser()
        {
            bool isNew = await UsersFirestore.IsNewUser(user_uid);
            Assert.False(isNew);
        }
    }
}
