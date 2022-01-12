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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AppUser>().HasMany(n => n.UserRoles).WithOne(n => n.User).HasForeignKey(n => n.UserId).IsRequired();

            modelBuilder.Entity<AppRole>().HasMany(n => n.UserRoles).WithOne(n => n.Role).HasForeignKey(n => n.RoleId).IsRequired();
        }
    }
}

