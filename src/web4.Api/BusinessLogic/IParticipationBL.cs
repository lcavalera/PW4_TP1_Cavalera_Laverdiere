using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic
{
    public interface IParticipationBL
    {
        public Task<List<ParticipationDTO>> ObtenirTout();
        public Task<ParticipationDTO?> ObtenirSelonId(int id);
        public Task Ajouter(ParticipationDTO demandeParticipation);
        public Task Supprimer(int id);
        public Task<bool> VerifierStatus(int id);
        public Task<List<ParticipationDTO>> ObtenirSelonEvenementId(int evenementId);
    }
}
