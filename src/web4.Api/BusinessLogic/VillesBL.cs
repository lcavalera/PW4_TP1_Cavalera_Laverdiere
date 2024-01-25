using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class VillesBL : IVillesBL
    {
        public IEnumerable<Ville> ObtenirTout()
        {
            return Repository.Villes.ToList();
        }
        public Ville? ObtenirSelonId(int id)
        {
            return Repository.Villes.FirstOrDefault(v => v.Id == id);
        }
        public Ville Ajouter(Ville ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            return Repository.AddVille(ville);
        }
        public void Modifier(int id, Ville ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            var villeAModifier = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (villeAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            villeAModifier.Nom = ville.Nom;
            villeAModifier.Region = ville.Region;
        }
        public void Supprimer(int id)
        {
            var ville = Repository.Villes.FirstOrDefault(v => v.Id == id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                if (Repository.Evenements.Any(e => e.VilleId == ville.Id))
                {
                    throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
                }

                Repository.Villes.Remove(ville);
            }
        }
    }
}
