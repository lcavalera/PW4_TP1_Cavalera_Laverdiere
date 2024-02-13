using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic
{
    public interface ICategorieBL
    {
        public Task<List<CategorieDTO>> ObtenirTout();
        public Task<CategorieDTO?> ObtenirSelonId(int id);
        public Task Ajouter(CategorieDTO categorie);
        public Task Modifier(int id, CategorieDTO categorie);
        public Task Supprimer(int id);
    }
}
