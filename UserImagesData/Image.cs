using System;
using System.Collections.Generic;
using System.Text;

namespace UserImagesData
{
    public class Image : BaseEntity
    {
        public string Name { get; set; }
        public byte[] Avatar { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsApproved { get; set; }
        public List<Notification> Notifications { get; set; }

        public Image()
        {
            Notifications = new List<Notification>();
        }
    }
}
