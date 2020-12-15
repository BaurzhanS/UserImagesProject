using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserImagesWeb.Models
{
    public class UserUpdateViewModel
    {
        [HiddenInput]
        public int UserId { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        public int RoleId { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
