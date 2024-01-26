using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface ICategorieBL
    {
        public List<Categorie> ObtenirTout();
        public Categorie? ObtenirSelonId(int id);
        public Categorie Ajouter(Categorie categorie);
        public void Modifier(int id, Categorie categorie);
        public void Supprimer(int id);
    }
}
