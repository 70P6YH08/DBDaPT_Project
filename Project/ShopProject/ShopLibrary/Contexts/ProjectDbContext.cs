using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopLibrary.Models;

namespace ShopLibrary.Contexts;

public partial class ProjectDbContext : DbContext
{
    public ProjectDbContext()
    {
    }

    public ProjectDbContext(DbContextOptions<ProjectDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Maker> Makers { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Shoe> Shoes { get; set; }

    public virtual DbSet<ShoeOrder> ShoeOrders { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<Vendor> Vendors { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("Server=localhost;Database=project_db;Uid=root;Pwd=;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Maker>(entity =>
        {
            entity.HasKey(e => e.MakerId).HasName("PRIMARY");

            entity.ToTable("maker");

            entity.Property(e => e.MakerId).HasColumnName("maker_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("order");

            entity.HasIndex(e => e.UserId, "user_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.DeliveryDate)
                .HasColumnType("date")
                .HasColumnName("delivery_date");
            entity.Property(e => e.IsFinished).HasColumnName("is_finished");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.ReceiveCode).HasColumnName("receive_code");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.User).WithMany(p => p.Orders)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_ibfk_1");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Shoe>(entity =>
        {
            entity.HasKey(e => e.ShoeId).HasName("PRIMARY");

            entity.ToTable("shoe");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.MakerId, "maker_id");

            entity.HasIndex(e => e.VendorId, "vendor_id");

            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.Article)
                .HasMaxLength(6)
                .IsFixedLength()
                .HasColumnName("article");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Color)
                .HasMaxLength(50)
                .HasColumnName("color");
            entity.Property(e => e.Description)
                .HasMaxLength(100)
                .HasColumnName("description");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.IsFemale).HasColumnName("is_female");
            entity.Property(e => e.MakerId).HasColumnName("maker_id");
            entity.Property(e => e.PhotoName)
                .HasMaxLength(50)
                .HasColumnName("photo_name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Size).HasColumnName("size");
            entity.Property(e => e.VendorId).HasColumnName("vendor_id");

            entity.HasOne(d => d.Category).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_ibfk_3");

            entity.HasOne(d => d.Maker).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.MakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_ibfk_2");

            entity.HasOne(d => d.Vendor).WithMany(p => p.Shoes)
                .HasForeignKey(d => d.VendorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_ibfk_1");
        });

        modelBuilder.Entity<ShoeOrder>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ShoeId }).HasName("PRIMARY");

            entity.ToTable("shoe_order");

            entity.HasIndex(e => e.ShoeId, "shoe_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ShoeId).HasColumnName("shoe_id");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.ShoeOrders)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_order_ibfk_1");

            entity.HasOne(d => d.Shoe).WithMany(p => p.ShoeOrders)
                .HasForeignKey(d => d.ShoeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("shoe_order_ibfk_2");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.RoleId, "role_id");

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.FirstName)
                .HasMaxLength(35)
                .HasColumnName("first_name");
            entity.Property(e => e.Login)
                .HasMaxLength(50)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(60)
                .IsFixedLength()
                .HasColumnName("password");
            entity.Property(e => e.Patronymic)
                .HasMaxLength(35)
                .HasColumnName("patronymic");
            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.SecondName)
                .HasMaxLength(35)
                .HasColumnName("second_name");

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("user_ibfk_1");
        });

        modelBuilder.Entity<Vendor>(entity =>
        {
            entity.HasKey(e => e.VendorId).HasName("PRIMARY");

            entity.ToTable("vendor");

            entity.Property(e => e.VendorId).HasColumnName("vendor_id");
            entity.Property(e => e.Name)
                .HasMaxLength(20)
                .HasColumnName("name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
