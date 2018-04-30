using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Exceptions;
using EasyCrud.Model.Interfaces;

namespace EasyCrud.Workflow
{
    public class UserWorkflow : IUserWorkflow
    {
        private readonly IUserRepository _userRepository;

        public UserWorkflow(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void CreateUser(string username, string plainPassword)
        {
            if (_userRepository.CheckIfUserExists(username))
            {
                throw new UserAlreadyExistException();
            }

            var keys = GetPasswordData(plainPassword);

            var user = new User
            {
                Username = username,
                Password = keys.Item1,
                Salt = keys.Item2
            };

            _userRepository.CreateUser(user);
        }

        private Tuple<string, string> GetPasswordData(string plainPassword)
        {
            using (var key = new Rfc2898DeriveBytes(plainPassword, 20, 1000))
            {
                return new Tuple<string, string>(Convert.ToBase64String(key.GetBytes(20)), Convert.ToBase64String(key.Salt));
            }
        }
    }
}
