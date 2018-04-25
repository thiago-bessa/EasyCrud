using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Interfaces;
using EasyCrud.Workflow;
using FluentAssertions;
using NUnit.Framework;

namespace EasyCrud.Tests
{
    public class UserTests
    {
        [Test]
        public void ExistingUserShouldThrowException()
        {
            var userRepository = new MockUserRepository();
            var userWorkflow = new UserWorkflow(userRepository);

            Action action = () => userWorkflow.CreateUser("existing_user", "anyPassword");
            action.Should().Throw<UserAlreadyExistException>("User already exists");
        }

        [Test]
        public void CreateUser()
        {
            var userRepository = new MockUserRepository();
            var userWorkflow = new UserWorkflow(userRepository);

            userWorkflow.CreateUser("easy", "crud");
            userRepository.CalledCreateUser.Should().BeTrue();
            userRepository.User.Username.Should().Be("easy");
        }
    }


    #region Mocks

    public class MockUserRepository : IUserRepository
    {
        public bool CalledCreateUser { get; private set; }
        public User User { get; private set; }

        public void CreateUser(User user)
        {
            CalledCreateUser = true;
            User = user;
        }

        public bool CheckIfUserExists(string username)
        {
            return username == "existing_user";
        }
    }

    #endregion
}
