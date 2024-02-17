using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface IStatistiquesBL
    {
        Task<List<string>> ObtenirVillesPopulairesAsync();
        Task<List<EvenementsProfitablesDTO>> ObtenirEvenementsProfitables();
    }
}
