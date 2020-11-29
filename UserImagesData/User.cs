using System;
using System.Collections.Generic;
using System.Text;

namespace UserImagesData
{
    public class User: BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? RoleId { get; set; }
        public Role Role { get; set; }
        public List<Image> Images { get; set; }
        public User()
        {
            Images = new List<Image>();
        }
    }
}
