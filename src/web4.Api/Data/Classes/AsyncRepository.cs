using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Microsoft.EntityFrameworkCore;

namespace Events.Api.Data.Classes
{
    public class AsyncRepository<TBaseEntity> : IAsyncRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        protected readonly EventsContext _context;

        public AsyncRepository(EventsContext context)
        {
            _context = context;
        }

        public async Task<TBaseEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TBaseEntity>().AsNoTracking().SingleOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<TBaseEntity>> ListAsync()
        {
            return await _context.Set<TBaseEntity>().AsNoTracking().ToListAsync();
        }

        public async Task AddAsync(TBaseEntity entity)
        {
            await _context.Set<TBaseEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TBaseEntity entity)
        {
            _context.Set<TBaseEntity>().Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task EditAsync(TBaseEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
