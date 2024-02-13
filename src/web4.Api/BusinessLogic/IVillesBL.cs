using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IVillesBL
    {
        Task<IEnumerable<Ville>> ObtenirTout();
        Task<Ville> ObtenirSelonId(int id);
        Task Ajouter(Ville ville);
        Task Modifier(int id, Ville ville);
        Task Supprimer(int id);
    }
}
