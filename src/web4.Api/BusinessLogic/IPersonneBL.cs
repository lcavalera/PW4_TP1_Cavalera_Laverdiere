using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IPersonneBL
    {
        public IEnumerable<Personne> ObtenirTout();
        public Personne? ObtenirSelonId(int id);
        public Personne Ajouter(Personne personne);
        public void Modifier(int id, Personne personne);
        public void Supprimer(int id);
    }
}
