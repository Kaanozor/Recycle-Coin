using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IEntityRepository<User> _UserRepository;
        public UserManager() { }

        public UserManager(IEntityRepository<User> userRepository)
        {
            _UserRepository = userRepository;
        }

        public User AddUser(User user)
        {
            _UserRepository.Add(user);
            return user;
        }

        public User GetUser(string Mail, string Password)
        {
            return _UserRepository.Get(x=> x.Mail==Mail && x.Password == Password);
        }

        public User UserGetUserByEmail(string Email)
        {
            return _UserRepository.Get(x => x.Mail == Email);
        }

        public User UserGetUserById(Guid guid)
        {
            return _UserRepository.Get(x => x.Id == guid);
        }
    }
}
