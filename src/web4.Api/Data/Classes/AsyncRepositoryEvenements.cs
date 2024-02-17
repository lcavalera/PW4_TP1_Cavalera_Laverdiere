using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data.Classes
{
    public class AsyncRepositoryEvenements : AsyncRepository<Evenement>, IAsyncRepositoryEvenements
    {
        public AsyncRepositoryEvenements(EventsContext context) : base(context)
        {
        }

        public async Task<int> GetTotal(int id)
        {
            Evenement? evenement = await _context.Set<Evenement>().Include(e => e.Participations).AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
            return evenement == null ? 0 : evenement.Prix * evenement.Participations.Sum(c => c.NombrePlaces);
        }

    }
}
