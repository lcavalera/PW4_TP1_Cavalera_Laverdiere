using Events.Api.Entites;
using System.Linq.Expressions;

namespace Events.Api.Data.Interfaces
{
    public interface IAsyncRepository<TBaseEntity> where TBaseEntity : BaseEntity
    {
        Task<TBaseEntity?> GetByIdAsync(int id);
        Task<IEnumerable<TBaseEntity>> ListAsync();
        Task AddAsync(TBaseEntity entity);
        Task DeleteAsync(TBaseEntity entity);
        Task EditAsync(TBaseEntity entity);
    }
}
