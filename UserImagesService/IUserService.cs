using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;

namespace UserImagesService
{
    public interface IUserService
    {
        IQueryable<User> GetUsers();
        User GetUser(long id);
        IQueryable<User> FindByCondition(Expression<Func<User, bool>> expression);
        void InsertUser(User user);
        void UpdateUser(User user);
        void DeleteUser(long id);
        
    }
}
