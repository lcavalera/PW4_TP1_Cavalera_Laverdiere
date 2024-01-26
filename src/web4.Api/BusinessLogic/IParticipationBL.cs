using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic
{
    public interface IParticipationBL
    {
        public List<Participation> ObtenirTout();
        public Participation? ObtenirSelonId(int id);
        public Participation Ajouter(DemandeParticipation demandeParticipation);
        public void Supprimer(int id);
        public bool VerifierStatus(int id);
        public List<Participation> ObtenirSelonEvenementId(int evenementId);
    }
}
