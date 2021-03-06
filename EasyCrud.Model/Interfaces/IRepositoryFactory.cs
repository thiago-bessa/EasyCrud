﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyCrud.Model.Database;

namespace EasyCrud.Model.Interfaces
{
    public interface IRepositoryFactory
    {
        IUserRepository GetUserRepository(EasyCrudContext context);
    }
}
