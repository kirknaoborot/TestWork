using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.IService;

namespace TestWork.Service
{
    public class UserService : IUserService
    {
        public bool CheckUser(string username, string password)
        {
            return username.Equals("User") && password.Equals("12345");
        }
    }
}
