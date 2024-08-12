using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WebMaster.Models
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<Sale> Sales { get; set; }

        public virtual DbSet<SalesProduct> SalesProducts { get; set; }

        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlServer("Server=tcp:webmasterabpos.database.windows.net,1433;Initial Catalog=dbPruebaTecnicaABPOS;Persist Security Info=False;User ID=johep;Password=Webmaster@24;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Products__3214EC0715817D4F");

                entity.Property(e => e.Name).HasMaxLength(100);
                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Sale>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Sales__3214EC073A9F71D8");

                entity.Property(e => e.Client).HasMaxLength(100);
                entity.Property(e => e.Contact).HasMaxLength(100);
                entity.Property(e => e.CreationDate).HasColumnType("datetime");
                entity.Property(e => e.Description).HasMaxLength(256);
                entity.Property(e => e.PaidDate).HasColumnType("datetime");
                entity.Property(e => e.TotalPrice).HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<SalesProduct>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__SalesPro__3214EC07F27F9C2E");

                entity.HasOne(d => d.Products).WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.ProductsId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalesProd__Produ__66603565");

                entity.HasOne(d => d.Sales).WithMany(p => p.SalesProducts)
                    .HasForeignKey(d => d.SalesId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__SalesProd__Sales__656C112C");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07EDAC5494");

                entity.Property(e => e.FirstName).HasMaxLength(100);
                entity.Property(e => e.LastName).HasMaxLength(100);
                entity.Property(e => e.Mail).HasMaxLength(256);
                entity.Property(e => e.Password).HasMaxLength(256);
                entity.Property(e => e.Type).HasMaxLength(100);
            });

        }
    }
}