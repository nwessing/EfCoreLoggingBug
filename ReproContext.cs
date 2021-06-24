using System;
using Microsoft.EntityFrameworkCore;

namespace EfSqlForeignKeyBug
{
    public class ReproContext : DbContext
    {
        public DbSet<AccessCodeEntity> AccessCodes { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            modelBuilder.Entity<AccessCodeEntity>(entity => {
                entity.ToTable("access_code");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.Code)

                    .HasColumnName("code")
                    .IsRequired();

                entity.Property(e => e.Email)
                    .HasColumnName("email");

                entity.Property(e => e.ParentAccessCodeId)
                    .HasColumnName("parentAccessCodeId");

                entity.HasOne(e => e.ParentAccessCode)
                    .WithMany(e => e.ChildAccessCodes)
                    .HasForeignKey(e => e.ParentAccessCodeId);

                entity.HasIndex(e => e.Code)
                    .IsUnique();
            });


            modelBuilder.Entity<UserEntity>(entity => {
                entity.ToTable("user");

                entity.Property(e => e.Id)
                    .HasColumnName("id")
                    .IsRequired();

                entity.Property(e => e.AccessCodeId)
                    .HasColumnName("accessCodeId")
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasColumnName("name")
                    .IsRequired();

                entity.HasOne(e => e.AccessCode)
                    .WithMany(e => e.Users)
                    .HasForeignKey(e => e.AccessCodeId);
            });
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=repro.db");
            options.LogTo(Console.WriteLine);
            options.EnableSensitiveDataLogging();
        }

        public void Migrate()
        {
            Database.Migrate();
        }
    }
}


