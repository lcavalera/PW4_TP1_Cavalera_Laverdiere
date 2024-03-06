using Events.Api.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data
{
    public class EventsContext: IdentityDbContext<IdentityUser>
    {
        public EventsContext(DbContextOptions<EventsContext> options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Identity data seed
            var managerRole = new IdentityRole { Id = "653bb917-ac53-464e-9e41-1be6fa6cf9e1", Name = "manager", NormalizedName = "MANAGER" };
            var adminRole = new IdentityRole { Id = "b4a17d23-2b27-4a35-950a-d97382cb90f4", Name = "admin", NormalizedName = "ADMIN" };
            
            var manager = new IdentityUser
            {
                Id = "3bd2f030-453b-45a1-89a2-9cade395d7c1",
                UserName = "manager@cegeplimoilou.ca",
                Email = "manager@cegeplimoilou.ca",
                NormalizedUserName = "MANAGER@CEGEPLIMOILOU.CA",
                NormalizedEmail = "MANAGER@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var admin = new IdentityUser
            {
                Id = "f389e134-488c-4fd5-b56c-9fb9f0b3b7f3",
                UserName = "admin@cegeplimoilou.ca",
                Email = "admin@cegeplimoilou.ca",
                NormalizedUserName = "ADMIN@CEGEPLIMOILOU.CA",
                NormalizedEmail = "ADMIN@CEGEPLIMOILOU.CA",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var hasher = new PasswordHasher<IdentityUser>();
            admin.PasswordHash = hasher.HashPassword(admin, "Admin123!");
            manager.PasswordHash = hasher.HashPassword(manager, "Manager123!");

            modelBuilder.Entity<IdentityRole>().HasData(managerRole);
            modelBuilder.Entity<IdentityRole>().HasData(adminRole);
            modelBuilder.Entity<IdentityUser>().HasData(manager);
            modelBuilder.Entity<IdentityUser>().HasData(admin);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = admin.Id, RoleId = adminRole.Id });
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> { UserId = manager.Id, RoleId = managerRole.Id });
            
            //configuration du model
            modelBuilder.Entity<Participation>().HasQueryFilter(p => p.EstValide);
            modelBuilder.Entity<Evenement>().HasMany(e => e.Participations).WithOne(p => p.Evenement).OnDelete(DeleteBehavior.Cascade);
        }

        public DbSet<Categorie>? Categories { get; set; }
        public DbSet<Ville>? Villes { get; set; }
        public DbSet<Participation>? Participations { get; set; }
        public DbSet<Evenement>? Evenements { get; set; }
    }
}
