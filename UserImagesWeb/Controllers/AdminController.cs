using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using UserImagesService;
using UserImagesWeb.Models;

namespace UserImagesWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IRoleService roleService;
        public AdminController(IRoleService roleService)
        {
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
            return View();
        }
    }
}
