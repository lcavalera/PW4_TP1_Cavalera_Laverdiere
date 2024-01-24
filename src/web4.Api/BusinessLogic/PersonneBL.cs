using Events.Api.Entites;
using Events.Api.Data;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class PersonneBL: IPersonneBL
    {
        public IEnumerable<Personne> ObtenirTout()
        {
            return Repository.Personnes.ToList();
        }
        public Personne? ObtenirSelonId(int id)
        {
            return Repository.Personnes.FirstOrDefault(p => p.Id == id);
        }
        public Personne Ajouter(Personne personne)
        {
            if (personne == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            return Repository.AddPersonne(personne);
        }
        public void Modifier(int id, Personne personne)
        {
            if (personne == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            var personneAModifier = Repository.Personnes.FirstOrDefault(p => p.Id == id);

            if (personneAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            personneAModifier.Nom = personne.Nom;
            personneAModifier.Telephone = personne.Telephone;
            personneAModifier.DateNaissance = personne.DateNaissance;
            personneAModifier.Courriel = personne.Courriel;
        }
        public void Supprimer(int id)
        {
            var personne = Repository.Personnes.FirstOrDefault(p => p.Id == id);

            if (personne == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                Repository.Personnes.Remove(personne);
            }
        }
    }
}
