using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class EvenementsBL: IEvenementsBL
    {
        public IEnumerable<Evenement> ObtenirTout()
        {
            return Repository.Evenements.ToList();
        }
        public Evenement? ObtenirSelonId(int id)
        {
            return Repository.Evenements.FirstOrDefault(e => e.Id == id);
        }
        public Evenement Ajouter(Evenement evenement)
        {
            if (evenement == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            if(evenement.DateDebut > evenement.DateDeFin)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "La date de fin doit être supérieur à la date de debut" } };
            }

            return Repository.AddEvenement(evenement);
        }
        public void Modifier(int id, Evenement evenement)
        {
            if (evenement == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            if (evenement.DateDebut > evenement.DateDeFin)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "La date de fin doit être supérieur à la date de debut" } };
            }

            var evenementAModifier = Repository.Evenements.FirstOrDefault(e => e.Id == id);

            if (evenementAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            evenementAModifier.DateDebut = evenement.DateDebut;
            evenementAModifier.DateDeFin = evenement.DateDeFin;
            evenementAModifier.Titre = evenement.Titre;
            evenementAModifier.Description = evenement.Description;
            evenementAModifier.Categories = evenement.Categories;
            evenementAModifier.Adresse = evenement.Adresse;
            evenementAModifier.NomOrganisateur = evenement.NomOrganisateur;
            evenementAModifier.Ville = evenement.Ville;
            evenementAModifier.Prix = evenement.Prix;

        }
        public void Supprimer(int id)
        {
            var evenement = Repository.Evenements.FirstOrDefault(e => e.Id == id);

            if (evenement == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                Repository.Evenements.Remove(evenement);
            }
        }
    }
}
