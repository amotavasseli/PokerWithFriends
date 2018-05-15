using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PokerWithFriends.Service.Domains;
using PokerWithFriends.Service.Services;
using PokerWithFriends.Web.Controllers;

namespace PokerWithFriends.Test
{
    [TestClass]
    public class UsersServiceTest
    {
        [TestMethod]
        public void TestUsersGetAllService()
        {
            var usersService = new UsersService();
            var users = usersService.GetAll();
            Assert.IsTrue(users.Count > 0, "there should be at least one user");
        }

        [TestMethod]
        public void TestUsersGetById()
        {
            var usersService = new UsersService();
            var user = usersService.GetUserById(1012);
            Assert.IsTrue(user.FirstName == "Arian");
            Assert.IsTrue(user.LastName == "Motavasseli");
            Assert.IsTrue(user.Email == "email@email.com");
        }
    }
}
