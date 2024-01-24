using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IEvenementsBL
    {
        public IEnumerable<Evenement> ObtenirTout();
        public Evenement? ObtenirSelonId(int id);
        public Evenement Ajouter(Evenement evenement);
        public void Modifier(int id, Evenement evenement);
        public void Supprimer(int id);
    }
}
