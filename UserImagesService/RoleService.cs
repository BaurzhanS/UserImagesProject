using System;
using System.Collections.Generic;
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

        public IEnumerable<Role> GetRoles()
        {
            return roleRepository.GetAll();
        }

        public Role GetRole(long id)
        {
            return roleRepository.Get(id);
        }

        public void InsertRole(Role role)
        {
            roleRepository.Insert(role);
        }
        public void UpdateRole(Role role)
        {
            roleRepository.Update(role);
        }

        public void DeleteRole(long id)
        {
            Role role = GetRole(id);
            roleRepository.Remove(role);
            roleRepository.SaveChanges();
        }
    }
}
