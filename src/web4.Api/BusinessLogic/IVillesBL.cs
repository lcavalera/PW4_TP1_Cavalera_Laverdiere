using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IVillesBL
    {
        public IEnumerable<Ville> ObtenirTout();
        public Ville? ObtenirSelonId(int id);
        public Ville Ajouter(Ville ville);
        public void Modifier(int id, Ville ville);
        public void Supprimer(int id);
    }
}
