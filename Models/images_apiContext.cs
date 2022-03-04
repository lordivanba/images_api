using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace images_api.Models
{
    public partial class images_apiContext : DbContext
    {
        public images_apiContext()
        {
        }

        public images_apiContext(DbContextOptions<images_apiContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Image> Images { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("name=bd");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Image>(entity =>
            {
                entity.ToTable("image");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Img)
                    .HasMaxLength(300)
                    .IsUnicode(false)
                    .HasColumnName("img");

                entity.Property(e => e.Title)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
