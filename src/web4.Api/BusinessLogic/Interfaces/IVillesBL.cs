using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface IVillesBL
    {
        Task<IEnumerable<VilleDTO>> ObtenirTout();
        Task<VilleDTO> ObtenirSelonId(int id);
        Task Ajouter(VilleDTO ville);
        Task Modifier(int id, VilleDTO ville);
        Task Supprimer(int id);
    }
}
