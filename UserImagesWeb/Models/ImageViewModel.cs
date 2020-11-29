using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserImagesWeb.Models
{
    public class ImageViewModel
    {
        public IFormFile Avatar { get; set; }
    }
}
