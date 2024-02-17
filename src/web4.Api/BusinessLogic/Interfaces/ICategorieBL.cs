using Events.Api.Entites;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Interfaces
{
    public interface ICategorieBL
    {
        Task<List<CategorieDTO>> ObtenirTout();
        Task<CategorieDTO?> ObtenirSelonId(int id);
        Task Ajouter(CategorieDTO categorie);
        Task Modifier(int id, CategorieDTO categorie);
        Task Supprimer(int id);
    }
}
