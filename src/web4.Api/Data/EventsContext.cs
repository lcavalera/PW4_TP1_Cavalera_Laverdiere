using Events.Api.Entites;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data
{
    public class EventsContext: DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options): base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            
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
