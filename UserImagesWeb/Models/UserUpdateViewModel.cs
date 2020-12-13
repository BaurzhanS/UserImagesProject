using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserImagesWeb.Models
{
    public class UserUpdateViewModel
    {
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Не указан Email")]
        public string Email { get; set; }

        [Display(Name = "Role")]
        public string RoleName { get; set; }

        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
