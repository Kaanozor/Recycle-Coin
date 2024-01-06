using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IUserService
    {
        User GetUser(string Mail,string Password);
        User UserGetUserByEmail(string Email);
        User AddUser(User user);
        User UserGetUserById(Guid guid);
    }
}
