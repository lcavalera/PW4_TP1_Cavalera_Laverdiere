using Events.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data
{
    public class AsyncRepositoryEvenements<TBaseEntity> : IAsyncRepository<Evenement> where TBaseEntity : Evenement
    {
        protected readonly EventsContext _context;

        public AsyncRepositoryEvenements(EventsContext context)
        {
            _context = context;
        }

        public async Task<Evenement> GetByIdAsync(int id)
        {
            return await _context.Set<Evenement>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Evenement>> ListAsync()
        {
            return await _context.Set<Evenement>().AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(Evenement entity)
        {
            await _context.Set<Evenement>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Evenement entity)
        {
            _context.Set<Evenement>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(Evenement entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetTotal(int id)
        {
            Evenement evenement = await _context.Set<Evenement>().Include(e=> e.Participations).AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
            return evenement.Prix * evenement.Participations.Sum(c => c.NombrePlaces);
        }
        //public async Task GetVillesPopulaires()
        //{
        //    var test = await _context.Set<Evenement>().ToListAsync();
        //    var meh = test.Select(c => c.VilleId);
        //    var h = meh.ToList();
        //    var t = h.Distinct();
        //    var s = t.Count();

        //}
    }
}
