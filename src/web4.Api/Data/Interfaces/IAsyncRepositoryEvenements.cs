using Events.Api.Entites;

namespace Events.Api.Data.Interfaces
{
    public interface IAsyncRepositoryEvenements : IAsyncRepository<Evenement>
    {
        Task<int> GetTotal(int id);
    }
}
