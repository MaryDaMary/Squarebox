﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace LimeBox.Models.Entities
{
    public partial class LimeContext : DbContext
    {
        public virtual DbSet<Boxes> Boxes { get; set; }
        public virtual DbSet<BoxTypes> BoxTypes { get; set; }
        public virtual DbSet<OrderRows> OrderRows { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(Startup.connString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Boxes>(entity =>
            {
                entity.ToTable("Boxes", "Lime");

                entity.Property(e => e.BoxId).HasColumnName("Box_Id");

                entity.Property(e => e.BoxPrice)
                    .HasColumnName("Box_Price")
                    .HasColumnType("decimal(18, 0)");

                entity.Property(e => e.BoxTypeId).HasColumnName("Box_Type_Id");

                entity.Property(e => e.BoxValue).HasColumnName("Box_Value");

                entity.HasOne(d => d.BoxType)
                    .WithMany(p => p.Boxes)
                    .HasForeignKey(d => d.BoxTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Boxes_ToTable_BoxTypes");
            });

            modelBuilder.Entity<BoxTypes>(entity =>
            {
                entity.ToTable("Box_Types", "Lime");

                entity.Property(e => e.BoxDescription)
                    .HasColumnName("Box_Description")
                    .HasMaxLength(1000);

                entity.Property(e => e.BoxImage)
                    .IsRequired()
                    .HasColumnName("Box_Image")
                    .HasMaxLength(1000);

                entity.Property(e => e.BoxImageHeader)
                    .HasColumnName("Box_Image_Header")
                    .HasMaxLength(1000);

                entity.Property(e => e.BoxType)
                    .IsRequired()
                    .HasColumnName("Box_type")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<OrderRows>(entity =>
            {
                entity.ToTable("Order_Rows", "Lime");

                entity.Property(e => e.BoxId).HasColumnName("Box_Id");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderRows)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Rows_ToTable");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "Lime");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.City)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UserId).HasColumnName("User_Id");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "Lime");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.AspNetId)
                    .IsRequired()
                    .HasColumnName("AspNet_Id")
                    .HasMaxLength(450);

                entity.Property(e => e.City).HasMaxLength(50);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(50);
            });
        }
    }
}
