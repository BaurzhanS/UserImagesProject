using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;

namespace UserImagesService
{
    public interface IRoleService
    {
        IQueryable<Role> GetRoles();
        Role GetRole(int id);
        IQueryable<Role> FindByCondition(Expression<Func<Role, bool>> expression);
        void InsertRole(Role role);
        void UpdateRole(Role role);
        void DeleteRole(int id);
    }

}
