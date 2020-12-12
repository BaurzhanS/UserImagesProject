﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace UserImagesData
{
    public class ImageMap
    {
        public ImageMap(EntityTypeBuilder<Image> entityBuilder)
        {
            entityBuilder.HasKey(t => t.Id);
            entityBuilder.Property(t => t.Name).IsRequired();
            entityBuilder.HasOne(t => t.User).WithMany(p => p.Images).HasForeignKey(t => t.UserId);
            entityBuilder.Property(t => t.IsApproved).HasDefaultValue(false);
        }
    }
}
