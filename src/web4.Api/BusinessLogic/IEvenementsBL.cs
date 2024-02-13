using Events.Api.Entites;

namespace Events.Api.BusinessLogic
{
    public interface IEvenementsBL
    {
        Task<IEnumerable<Evenement>> ObtenirTout();
        Task<Evenement> ObtenirSelonId(int id);
        Task<IEnumerable<Evenement>> ObtenirSelonIdVille(int villeId);
        Task Ajouter(Evenement evenement);
        Task Modifier(int id, Evenement evenement);
        Task Supprimer(int id);
    }
}
