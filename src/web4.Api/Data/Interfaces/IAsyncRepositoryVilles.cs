using Events.Api.Entites;

namespace Events.Api.Data.Interfaces
{
    public interface IAsyncRepositoryVilles : IAsyncRepository<Ville>
    {
        Task<List<Ville>> GetVillesEvenementsAsync();
    }
}
