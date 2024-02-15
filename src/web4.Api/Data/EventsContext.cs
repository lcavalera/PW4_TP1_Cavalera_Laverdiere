using Events.Api.Entites;
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
            modelBuilder.Entity<Evenement>().HasMany(e => e.Participations).WithOne(p => p.Evenement).OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Evenement>().HasQueryFilter(p => EF.Property<string>(p, "filtre") != null);
            //modelBuilder.Entity<Evenement>().HasQueryFilter(p => p.Titre != null || p.Description != null);
        }

        public DbSet<Categorie>? Categories { get; set; }
        public DbSet<Ville>? Villes { get; set; }
        public DbSet<Participation>? Participations { get; set; }
        public DbSet<Evenement>? Evenements { get; set; }
    }
}
