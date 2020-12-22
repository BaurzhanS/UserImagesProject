using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using UserImagesData;

namespace UserImagesRepo
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            new UserMap(modelBuilder.Entity<User>());
            new RoleMap(modelBuilder.Entity<Role>());
            new ImageMap(modelBuilder.Entity<Image>());
            new NotificationMap(modelBuilder.Entity<Notification>());
        }
    }
}
