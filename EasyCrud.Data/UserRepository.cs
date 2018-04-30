using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Interfaces;

namespace EasyCrud.Data
{
    public class UserRepository : IUserRepository
    {
        private readonly EasyCrudContext _databaseContext;

        public UserRepository(EasyCrudContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void CreateUser(User user)
        {
            _databaseContext.Users.Add(user);
            _databaseContext.SaveChanges();
        }
        
        public bool CheckIfUserExists(string username)
        {
            return _databaseContext.Users.Any(user => user.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
