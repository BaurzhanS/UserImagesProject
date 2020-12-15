using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserImagesWeb.Models
{
    public class ImageUpdateViewModel
    {
        [HiddenInput]
        public int Id { get; set; }

        public string Name { get; set; }

        public byte[] Image { get; set; }

        public IFormFile Avatar { get; set; }

        public string UserEmail { get; set; }
    }
}
