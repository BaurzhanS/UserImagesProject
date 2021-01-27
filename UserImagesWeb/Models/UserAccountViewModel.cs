using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserImagesData;

namespace UserImagesWeb.Models
{
    public class UserAccountViewModel
    {
        public User User { get; set; }

        public List<Notification> Notifications { get; set; }
    }

}
