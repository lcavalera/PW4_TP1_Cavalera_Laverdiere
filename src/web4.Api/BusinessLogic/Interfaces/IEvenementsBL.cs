using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface IEvenementsBL
    {
        Task<IEnumerable<EvenementDTO>> ObtenirTout();
        Task<EvenementDTO> ObtenirSelonId(int id);
        Task<IEnumerable<EvenementDTO>> ObtenirSelonIdVille(int villeId);
        Task Ajouter(EvenementDTO evenement);
        Task Modifier(int id, EvenementDTO evenement);
        Task Supprimer(int id);
    }
}
