using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserImagesData
{
    public class NotificationMap
    {
        public NotificationMap(EntityTypeBuilder<Notification> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Message).IsRequired();
            entityBuilder.HasOne(t => t.User).WithMany(p => p.Notifications).HasForeignKey(t => t.UserId);
            entityBuilder.HasOne(t => t.Image).WithMany(p => p.Notifications).HasForeignKey(t => t.ImageId);
            entityBuilder.Property(t => t.IsRead).HasDefaultValue(false);
        }
    }
}
