using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic
{
    public interface IParticipationBL
    {
        public IEnumerable<Participation> ObtenirTout();
        public Participation? ObtenirSelonId(int id);
        public Participation Ajouter(DemandeParticipation demandeParticipation);
        public void Modifier(int id, Participation participation);
        public void Supprimer(int id);
    }
}
