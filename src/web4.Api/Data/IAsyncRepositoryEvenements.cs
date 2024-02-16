using Events.Api.Entites;

namespace Events.Api.Data
{
    public interface IAsyncRepositoryEvenements: IAsyncRepository<Evenement>
    {
        Task<int> GetTotal(int id);
    }
}
