using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using UserImagesData;
using UserImagesService;
using UserImagesWeb.Models;
using UserImagesWeb.Services;

namespace UserImagesWeb.Controllers
{
    public class AdminController : Controller
    {
        private readonly IUserService userService;
        private readonly IRoleService roleService;
        private readonly INotificationService notificationService;
        private readonly IImageService imageService;
        private readonly IHubContext<NotificationUserHub> notificationUserHubContext;
        private readonly IUserConnectionManager userConnectionManager;

        public AdminController(IRoleService roleService, IUserService userService, INotificationService notificationService, IImageService imageService, IHubContext<NotificationUserHub> notificationUserHubContext, IUserConnectionManager userConnectionManager)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.notificationService = notificationService;
            this.imageService = imageService;
            this.notificationUserHubContext = notificationUserHubContext;
            this.userConnectionManager = userConnectionManager;
        }

        public IActionResult RegisterAdmin()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RegisterAdmin(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userService.FindByCondition(p => p.Email == model.Email).Include(p => p.Role).
                    FirstOrDefault(p => p.Role.Name == "admin");

                if (user != null)
                {
                    throw new Exception("Admin already exists");
                }

                if (user == null)
                {
                    user = new User { Email = model.Email, Password = model.Password, UserName = model.LastName + " " + model.FirstName };

                    Role userRole = roleService.FindByCondition(p => p.Name == "admin").FirstOrDefault();

                    if (userRole == null)
                    {
                        throw new Exception("There is no user role (admin must add it)");
                    }

                    user.Role = userRole;
                    user.RoleId = userRole.Id;

                    userService.InsertUser(user);

                    return RedirectToAction("UsersList");
                }
                else
                    ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
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

        [HttpGet]
        public IActionResult EditUser(int? id)
        {
            var userViewModel = new UserUpdateViewModel();

            var user = userService.GetUser(id.Value);

            var userRole = roleService.GetRole(user.RoleId.Value);

            if (userRole == null)
            {
                throw new Exception("No role exists with this id");
            }

            if (user == null)
            {
                throw new Exception("No user exists with this email");
            }

            userViewModel.UserId = user.Id;
            userViewModel.Email = user.Email;
            userViewModel.FullName = user.UserName;
            userViewModel.Password = user.Password;
            userViewModel.RoleId = user.RoleId.Value;

            var userRoles = new SelectList(roleService.GetRoles().ToList(), "Id", "Name");

            ViewBag.Roles = userRoles;

            return View(userViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditUser(UserUpdateViewModel model)
        {
            var user = userService.FindByCondition(p => p.Id == model.UserId).FirstOrDefault();

            if (user == null)
            {
                throw new Exception("No user exists with this email");
            }

            var userRole = roleService.FindByCondition(p => p.Id == model.RoleId).FirstOrDefault();
            user.Email = model.Email;
            user.UserName = model.FullName;
            user.RoleId = userRole.Id;

            userService.UpdateUser(user);
            return RedirectToAction("UsersList");
        }

        [HttpGet]
        public IActionResult DeleteUser(int? id)
        {
            var user = userService.GetUser(id.Value);

            if (user == null)
            {
                throw new Exception("No user exists with this email");
            }

            userService.DeleteUser(user.Id);

            return RedirectToAction("UsersList");
        }

        [HttpGet]
        public IActionResult NotificationsList()
        {
            var notifications = notificationService.FindNotificationByCondition(p => p.IsRead == false).Include(p => p.Image).Include(p => p.User);

            return View(notifications);
        }

        [HttpGet]
        public IActionResult ReadNotification(int? id)
        {
            var notification = notificationService.GetNotification(id.Value);
            notification.IsRead = true;
            notificationService.UpdateNotification(notification);

            return RedirectToAction("NotificationsList");
        }

        [HttpGet]
        public IActionResult ImagesToApprove()
        {
            var images = imageService.FindImageByCondition(p => p.IsApproved == false).Include(p => p.User);
            return View(images);
        }

        [HttpGet]
        public async Task<IActionResult> ApproveImage(int? id)
        {
            var image = imageService.FindImageByCondition(p => p.IsApproved == false && p.Id == id).Include(p => p.User).FirstOrDefault();

            if (image is null)
            {
                throw new Exception("Image is not found");
            }

            //image.IsApproved = true;
            //imageService.UpdateImage(image);

            var connections = userConnectionManager.GetUserConnections(image.UserId.ToString());

            if(connections is null)
            {
                //var notification = new Notification
                //{
                //    ImageId = image.Id,
                //    UserId = image.UserId,
                //    Message = $"Модератор одобрил Вашу аватарку {image.Name}"
                //};
            }

            if (connections != null && connections.Count > 0)
            {
                foreach (var connectionId in connections)
                {
                    await notificationUserHubContext.Clients.Client(connectionId).SendAsync("sendToUser", $"Аватарка {image.Name}", "Ваша аватарка одобрена модератором!");
                }
            }

            return Ok();
        }
    }
}
