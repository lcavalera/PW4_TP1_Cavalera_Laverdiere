using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IUsagersBL
    {
        public IEnumerable<Usager> ObtenirTout();
        public Usager? ObtenirSelonId(int id);
        public Usager Ajouter(Usager usager);
        public void Modifier(int id, Usager usager);
        public void Supprimer(int id);
    }
}
