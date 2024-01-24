using Events.Api.Entites;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class UsagersBL: IUsagersBL
    {
        public IEnumerable<Usager> ObtenirTout()
        {
            return Repository.Usagers.ToList();
        }
        public Usager? ObtenirSelonId(int id)
        {
            return Repository.Usagers.FirstOrDefault(u => u.Id == id);
        }
        public Usager Ajouter(Usager usager)
        {
            if (usager == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            return Repository.AddUsager(usager);
        }
        public void Modifier(int id, Usager usager)
        {
            if (usager == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            var usagerAModifier = Repository.Usagers.FirstOrDefault(u => u.Id == id);

            if (usagerAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            usagerAModifier.Nom = usager.Nom;
            usagerAModifier.Prenom = usager.Prenom;
            usagerAModifier.Telephone = usager.Telephone;
            usagerAModifier.DateNaissance = usager.DateNaissance;
            usagerAModifier.Courriel = usager.Courriel;
        }
        public void Supprimer(int id)
        {
            var usager = Repository.Usagers.FirstOrDefault(u => u.Id == id);

            if (usager == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                Repository.Usagers.Remove(usager);
            }
        }
    }
}
