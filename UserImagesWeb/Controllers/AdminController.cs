using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserImagesData;
using UserImagesService;
using UserImagesWeb.Models;

namespace UserImagesWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;

        public AdminController(IRoleService roleService, IUserService userService)
        {
            this.userService = userService;
            this.roleService = roleService;
        }
        public IActionResult RoleList()
        {
            var roles = roleService.GetRoles();
            return View(roles);
        }

        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateRole(RoleViewModel roleViewModel)
        {
            if (roleViewModel != null)
            {
                var role = new Role
                {
                    Name = roleViewModel.RoleName
                };

                roleService.InsertRole(role);
            }

            return Redirect("RoleList");
        }

        public IActionResult UsersList()
        {
            var users = userService.GetUsers().Include(p => p.Role);

            return View(users);
        }
    }
}
