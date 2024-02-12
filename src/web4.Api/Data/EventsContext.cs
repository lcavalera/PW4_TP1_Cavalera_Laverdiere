using Events.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data
{
    public class EventsContext: DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options): base(options)
        {

        }

        public DbSet<Categorie>? Categories { get; set; }
        public DbSet<Ville>? Villes { get; set; }
        public DbSet<Participation>? Participations { get; set; }
        public DbSet<Evenement>? Evenements { get; set; }
    }
}
