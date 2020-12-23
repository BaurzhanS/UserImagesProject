using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UserImagesData;
using UserImagesService;
using UserImagesWeb.Models;

namespace UserImagesWeb.Controllers
{
    public class ImageController : Controller
    {
        private readonly IImageService imageService;
        private readonly IUserService userService;
        private readonly INotificationService notificationService;

        public ImageController(IImageService imageService, IUserService userService, INotificationService notificationService)
        {
            this.imageService = imageService;
            this.userService = userService;
            this.notificationService = notificationService;
        }
        public IActionResult Index()
        {
            var images = imageService.GetImages().Include(p => p.User);
            return View(images);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(ImageViewModel imageViewModel)
        {
            if (ModelState.IsValid)
            {
                var image = new Image { Name = imageViewModel.Name };

                var user = userService.FindByCondition(p => p.Email == imageViewModel.UserEmail).FirstOrDefault();
                if (user == null)
                {
                    throw new Exception("No user exists with this email");
                }
                if (imageViewModel.Avatar != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(imageViewModel.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)imageViewModel.Avatar.Length);
                    }
                    // установка массива байтов
                    image.Avatar = imageData;
                }
                image.UserId = user.Id;

                imageService.InsertImage(image);

                //var notification = new NotificationViewModel();
                var notification = new Notification();
                notification.Message = $"Пользователь {user.Email} загрузил аватарку {image.Name}";
                notification.UserId = user.Id;
                notification.ImageId = image.Id;

                notificationService.InsertNotification(notification);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            var imageUpdateViewModel = new ImageUpdateViewModel();

            var image = imageService.GetImage(id.Value);

            var user = userService.GetUser(image.UserId);

            if (image == null)
            {
                throw new Exception("No image exists with this id");
            }

            if (user == null)
            {
                throw new Exception("No user exists with this image");
            }

            imageUpdateViewModel.UserEmail = user.Email;
            imageUpdateViewModel.Name = image.Name;
            imageUpdateViewModel.Image = image.Avatar;

            return View("Edit", imageUpdateViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ImageUpdateViewModel model)
        {
            var image = imageService.GetImage(model.Id);

            if (image != null)
            {
                image.Name = model.Name;

                if (model.Avatar != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(model.Avatar.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)model.Avatar.Length);
                    }
                    // установка массива байтов
                    image.Avatar = imageData;
                }
                imageService.UpdateImage(image);
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            var image = imageService.GetImage(id.Value);

            if (image != null)
            {
                imageService.DeleteImage(image.Id);
            }

            return RedirectToAction("Index");
        }
    }
}
