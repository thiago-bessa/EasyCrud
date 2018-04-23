using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyCrud.Model.Interfaces
{
    public interface IUserWorkflow
    {
        void CreateUser(string username, string plainPassword);
    }
}
