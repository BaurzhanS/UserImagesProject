using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserImagesWeb.Models
{
    public class NotificationViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Message { get; set; }
        public string UserEmail { get; set; }
       
        public string ImageName { get; set; }
    }
}
