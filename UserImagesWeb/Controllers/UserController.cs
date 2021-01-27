using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserImagesData;
using UserImagesService;
using UserImagesWeb.Models;
using UserImagesWeb.Services;

namespace UserImagesWeb.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly INotificationService notificationService;
        private readonly IUserConnectionManager userConnectionManager;

        public UserController(IUserService userService, IRoleService roleService, INotificationService notificationService, IUserConnectionManager userConnectionManager)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.notificationService = notificationService;
            this.userConnectionManager = userConnectionManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.FindByCondition(p => p.Email == model.Email).FirstOrDefault();
                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password, UserName = model.LastName + " " + model.FirstName };

                    Role userRole = roleService.FindByCondition(p => p.Name == "user").FirstOrDefault();

                    if (userRole == null)
                    {
                        throw new Exception("There is no user role (admin must add it)");
                    }

                    user.Role = userRole;
                    user.RoleId = userRole.Id;

                    userService.InsertUser(user);

                    await Authenticate(user);

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.FindByCondition(p=>p.Email==model.Email).Include(p=>p.Role).FirstOrDefault();

                if (user == null)
                {
                    throw new Exception("There is no such user");
                }

                await Authenticate(user);

                return RedirectToAction("Index", "Home");

            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Authorize(Roles = "admin, user")]
        public IActionResult UserAccount()
        {
            string userEmail = HttpContext.User.Identity.Name;
            var user = userService.FindByCondition(p => p.Email == userEmail).Include(p => p.Role).Include(p=>p.Images).FirstOrDefault();

            var notifications = notificationService.FindNotificationByCondition(p => p.UserId == user.Id && p.ToModerator == false && p.IsRead == false).ToList();

            UserAccountViewModel userAccountViewModel = new UserAccountViewModel
            {
                User = user,
                Notifications = notifications
            };

            return View(userAccountViewModel);
        }

        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name)
            };

            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        [HttpGet]
        public IActionResult ReadNotification(int? id)
        {
            var notification = notificationService.GetNotification(id.Value);
            notification.IsRead = true;
            notificationService.UpdateNotification(notification);

            return RedirectToAction("UserAccount");
        }
    }
}
