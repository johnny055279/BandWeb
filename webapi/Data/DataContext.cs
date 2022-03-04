using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using webapi.Entities;
using webapi.Interfaces;

namespace webapi.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DataContext(DbContextOptions options) : base(options) { }

        public DbSet<Ticket> Tickets { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<PostComment> PostComments { get; set; }

        public DbSet<PostLike> PostLikes { get; set; }

        public DbSet<UserTicketOrder> UserTicketOrders { get; set; }

        public DbSet<News> News { get; set; }

        public DbSet<City> City { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasMany(n => n.UserRoles).WithOne(n => n.User).HasForeignKey(n => n.UserId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppRole>().HasMany(n => n.UserRoles).WithOne(n => n.Role).HasForeignKey(n => n.RoleId).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>().HasMany(n => n.PostComments).WithOne(n => n.User).HasForeignKey(n => n.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>().HasMany(n => n.PostLikes).WithOne(n => n.User).HasForeignKey(n => n.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<AppUser>().HasMany(n => n.TicketOrders).WithOne(n => n.User).HasForeignKey(n => n.Id).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>().HasMany(n => n.PostComment).WithOne(n => n.Post).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Post>().HasMany(n => n.PostLike).WithOne(n => n.Post).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Ticket>().HasMany(n => n.TicketOrders).WithOne(n => n.Ticket).IsRequired().OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<News>();

            modelBuilder.Entity<Ticket>().HasOne(n => n.City);

        }
    }
}

