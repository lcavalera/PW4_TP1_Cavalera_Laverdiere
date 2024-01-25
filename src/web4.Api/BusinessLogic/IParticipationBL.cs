using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IParticipationBL
    {
        public IEnumerable<Participation> ObtenirTout();
        public Participation? ObtenirSelonId(int id);
        public Participation Ajouter(Participation participation);
        public void Modifier(int id, Participation participation);
        public void Supprimer(int id);
    }
}
