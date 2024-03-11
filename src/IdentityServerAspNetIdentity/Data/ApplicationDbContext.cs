using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using IdentityServerAspNetIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.Reflection.Emit;
using System;

namespace IdentityServerAspNetIdentity.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
            //Identity data seed
            var managerRole = new IdentityRole { Id = "653bb917-ac53-464e-9e41-1be6fa6cf9e1", Name = "manager", NormalizedName = "MANAGER" };
            var adminRole = new IdentityRole { Id = "b4a17d23-2b27-4a35-950a-d97382cb90f4", Name = "admin", NormalizedName = "ADMIN" };

            var manager = new ApplicationUser
            {
                Id = "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                UserName = "manager@cegeplimoilou.ca",
                Email = "manager@cegeplimoilou.ca",
                NormalizedUserName = "MANAGER@CEGEPLIMOILOU.CA",
                NormalizedEmail = "MANAGER@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
            };

            var admin = new ApplicationUser
            {
                Id = "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                UserName = "admin@cegeplimoilou.ca",
                Email = "admin@cegeplimoilou.ca",
                NormalizedUserName = "ADMIN@CEGEPLIMOILOU.CA",
                NormalizedEmail = "ADMIN@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
            };

            var hasher = new PasswordHasher<ApplicationUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            manager.PasswordHash = hasher.HashPassword(manager, "Manager123!");

            builder.Entity<IdentityRole>().HasData(managerRole);
            builder.Entity<IdentityRole>().HasData(adminRole);
            builder.Entity<ApplicationUser>().HasData(manager);
            builder.Entity<ApplicationUser>().HasData(admin);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id });
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = manager.Id, RoleId = managerRole.Id });
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
