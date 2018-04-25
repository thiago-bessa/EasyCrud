using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;
using EasyCrud.Model.Interfaces;

namespace EasyCrud.DAO
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IUserRepository GetUserRepository(EasyCrudContext context)
        {
            return new UserRepository(context);
        }
    }
}
