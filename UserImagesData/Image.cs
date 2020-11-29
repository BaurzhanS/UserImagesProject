using System;
using System.Collections.Generic;
using System.Text;

namespace UserImagesData
{
    public class Image: BaseEntity
    {
        public byte[] Name { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
