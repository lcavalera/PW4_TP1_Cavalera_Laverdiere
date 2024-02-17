using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface IParticipationBL
    {
        Task<List<ParticipationDTO>> ObtenirTout();
        Task<ParticipationDTO?> ObtenirSelonId(int id);
        Task Ajouter(ParticipationDTO demandeParticipation);
        Task Supprimer(int id);
        Task<bool> VerifierStatus(int id);
        Task<List<ParticipationDTO>> ObtenirSelonEvenementId(int evenementId);
    }
}
