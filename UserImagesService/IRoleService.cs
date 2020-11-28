using System;
using System.Collections.Generic;
using System.Text;
using UserImagesData;

namespace UserImagesService
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
        Role GetRole(long id);
        void InsertRole(Role user);
        void UpdateRole(Role user);
        void DeleteRole(long id);
    }
}
