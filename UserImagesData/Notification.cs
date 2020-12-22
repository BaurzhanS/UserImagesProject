using System;
using System.Collections.Generic;
using System.Text;

namespace UserImagesData
{
    public class Notification : BaseEntity
    {
        public string Message { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public int? ImageId { get; set; }
        public Image Image { get; set; }
        public bool IsRead { get; set; }
    }
}
