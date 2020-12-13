using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;
using UserImagesRepo;

namespace UserImagesService
{
    public class UserService : IUserService
    {
        private IRepository<User> userRepository;

        public UserService(IRepository<User> userRepository)
        {
            this.userRepository = userRepository;
        }

        public IQueryable<User> GetUsers()
        {
            return userRepository.GetAll();
        }

        public User GetUser(int id)
        {
            return userRepository.Get(id);
        }

        public IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression)
        {
            return userRepository.FindByCondition(expression);
        }

        public void InsertUser(User user)
        {
            userRepository.Insert(user);
        }
        public void UpdateUser(User user)
        {
            userRepository.Update(user);
        }

        public void DeleteUser(int id)
        {
            User user = GetUser(id);
            userRepository.Remove(user);
            userRepository.SaveChanges();
        }
    }
}
