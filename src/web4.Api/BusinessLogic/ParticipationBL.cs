using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class ParticipationBL(IEvenementsBL evenementBL) : IParticipationBL
    {
        private readonly IEvenementsBL _evenementBL = evenementBL;

        public Participation Ajouter(DemandeParticipation demandeParticipation)
        {
            if (demandeParticipation == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            if (demandeParticipation.NombrePlaces == 0)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Nombre de places doit être supérieur à zéro" } };
            }
            if (demandeParticipation.Prenom == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Renseignement du prenom est obligatoire pour participer à un évènement" } };
            }
            if (demandeParticipation.Nom == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Renseignement du nom est obligatoire pour participer à un évènement" } };
            }
            if (demandeParticipation.Courriel == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Renseignement du courriel est obligatoire pour participer à un évènement" } };
            }
            Evenement? evenement = _evenementBL.ObtenirSelonId(demandeParticipation.EvenementID);
            if (evenement == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Évènement introuvable" } };
            }
            bool participeDeja = Repository.Participations.Any(p => p.Courriel == demandeParticipation.Courriel && p.EvenementID == demandeParticipation.EvenementID);
            if (participeDeja)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Cette adresse électronique participe déjà à cet Évènement" } };
            }
            Participation participation = new()
            {
                Courriel = demandeParticipation.Courriel,
                Nom = demandeParticipation.Nom,
                Prenom = demandeParticipation.Prenom,
                EvenementID = demandeParticipation.EvenementID,
                NombrePlaces = demandeParticipation.NombrePlaces
            };

            return Repository.AddParticipation(participation);
        }

        public void Modifier(int id, Participation participation)
        {
            if ()
            {

            }
        }

        public Participation? ObtenirSelonId(int id)
        {
            return Repository.Participations.FirstOrDefault(x => x.Id == id);
        }

        public IEnumerable<Participation> ObtenirTout()
        {
            return Repository.Participations.ToList();
        }

        public void Supprimer(int id)
        {
            Participation? participation = Repository.Participations.FirstOrDefault(x => x.Id == id);
            if (participation == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                Repository.Participations.Remove(participation);
            }
        }
        public bool VerifierStatus(int id)
        {
            Participation? participation = ObtenirSelonId(id);
            if (participation == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Participation introuvable (id={id})" } };
            }
            SimulerVerifierStatus(participation);
            return participation.EstValide;
        }
        private void SimulerVerifierStatus(Participation participation)
        {

        }
    }
}
