using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasMany(n => n.UserRoles).WithOne(n => n.User).HasForeignKey(n => n.UserId).IsRequired();

            modelBuilder.Entity<AppRole>().HasMany(n => n.UserRoles).WithOne(n => n.Role).HasForeignKey(n => n.RoleId).IsRequired();

            modelBuilder.Entity<AppUser>().HasMany(n => n.PostComments).WithOne(n => n.User).HasForeignKey(n => n.Id).IsRequired();

            modelBuilder.Entity<AppUser>().HasMany(n => n.PostLikes).WithOne(n => n.User).HasForeignKey(n => n.Id).IsRequired();

            modelBuilder.Entity<PostComment>().HasOne(n => n.Post).WithMany(n => n.PostComment).IsRequired();

            modelBuilder.Entity<PostLike>().HasOne(n => n.Post).WithMany(n => n.PostLike).IsRequired();
        }
    }
}

