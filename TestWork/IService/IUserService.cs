using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestWork.IService
{
    public interface IUserService
    {
       public bool CheckUser(string username, string password);
    }
}
