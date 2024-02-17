using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data.Classes
{
    public class AsyncRepositoryVilles : AsyncRepository<Ville>, IAsyncRepositoryVilles
    {
        public AsyncRepositoryVilles(EventsContext context) : base(context)
        {
        }
        public async Task<List<Ville>> GetVillesEvenementsAsync()
        {
            return await _context.Set<Ville>().Include(v => v.Evenements).ToListAsync();

            //var villes = await _context.Set<Ville>().Include(v => v.Evenements).ToListAsync();
            //var villesOrdreParCount = villes.OrderByDescending(c => c.Evenements.Count);
            //return villesOrdreParCount.Select(c => c.Nom).Take(10).ToList();
        }
    }
}
