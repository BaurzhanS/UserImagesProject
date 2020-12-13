using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using UserImagesData;
using UserImagesRepo;

namespace UserImagesService
{
    public class RoleService : IRoleService
    {
        private IRepository<Role> roleRepository;

        public RoleService(IRepository<Role> roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        public IQueryable<Role> GetRoles()
        {
            return roleRepository.GetAll();
        }

        public Role GetRole(int id)
        {
            return roleRepository.Get(id);
        }

        public IQueryable<Role> FindByCondition(Expression<Func<Role, bool>> expression)
        {
            return roleRepository.FindByCondition(expression);
        }
        public void InsertRole(Role role)
        {
            roleRepository.Insert(role);
        }
        public void UpdateRole(Role role)
        {
            roleRepository.Update(role);
        }

        public void DeleteRole(int id)
        {
            Role role = GetRole(id);
            roleRepository.Remove(role);
            roleRepository.SaveChanges();
        }
    }
}
