using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface IStatistiquesBL
    {
        Task<List<VillesPopulairesDTO>> ObtenirVillesPopulairesAsync();
        Task<List<EvenementsProfitablesDTO>> ObtenirEvenementsProfitables();
    }
}
