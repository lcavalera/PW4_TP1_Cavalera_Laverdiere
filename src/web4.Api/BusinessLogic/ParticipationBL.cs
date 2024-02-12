using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;
using System.Collections.Generic;

namespace Events.Api.BusinessLogic
{
    public class ParticipationBL(IEvenementsBL evenementBL, IAsyncRepository<Participation> participationRepo) : IParticipationBL
    {
        private readonly IEvenementsBL _evenementBL = evenementBL;
        private readonly IAsyncRepository<Participation> _participationRepo = participationRepo;

        public async Task Ajouter(ParticipationDTO demandeParticipation)
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
            Evenement? evenement = await _evenementBL.ObtenirSelonId(demandeParticipation.EvenementID);
            bool participeDeja = Repository.Participations.Any(p => p.Courriel == demandeParticipation.Courriel && p.EvenementID == demandeParticipation.EvenementID);
            if (participeDeja)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Cette adresse électronique participe déjà à cet Évènement" } };
            }
            
            await _participationRepo.AddAsync(new Participation()
            {
                Courriel = demandeParticipation.Courriel,
                Nom = demandeParticipation.Nom,
                Prenom = demandeParticipation.Prenom,
                EvenementID = demandeParticipation.EvenementID,
                NombrePlaces = demandeParticipation.NombrePlaces
            });
        }

        public async Task<List<ParticipationDTO>> ObtenirSelonEvenementId(int evenementId)
        {
            IEnumerable<Participation>? participations = await _participationRepo.ListAsync();
            return participations.Where(p => p.EvenementID == evenementId).Select(p => new ParticipationDTO { Id = p.Id, Nom = p.Nom, Prenom = p.Prenom, Courriel = p.Courriel, EstValide = p.EstValide, EvenementID = p.EvenementID, NombrePlaces = p.NombrePlaces }).ToList();
        }

        public async Task<ParticipationDTO?> ObtenirSelonId(int id)
        {
            Participation? participation = await _participationRepo.GetByIdAsync(id);
            if (participation == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            return new ParticipationDTO { Id = participation.Id, Nom = participation.Nom, Prenom = participation.Prenom, Courriel = participation.Courriel, EstValide = participation.EstValide, EvenementID = participation.EvenementID, NombrePlaces = participation.NombrePlaces };
        }

        public async Task<List<ParticipationDTO>> ObtenirTout()
        {
            IEnumerable<Participation>? participations = await _participationRepo.ListAsync();
            return participations.Select(p => new ParticipationDTO { Id = p.Id, Nom = p.Nom, Prenom = p.Prenom, Courriel = p.Courriel, EstValide = p.EstValide, EvenementID = p.EvenementID, NombrePlaces = p.NombrePlaces }).ToList();
        }

        public async Task Supprimer(int id)
        {
            ParticipationDTO? participation = await ObtenirSelonId(id);
            await _participationRepo.DeleteAsync(new Participation { Id = participation.Id, Nom = participation.Nom, Prenom = participation.Prenom, Courriel = participation.Courriel, EstValide = participation.EstValide, EvenementID = participation.EvenementID, NombrePlaces = participation.NombrePlaces });
        }
        public async Task<bool> VerifierStatus(int id)
        {
            ParticipationDTO? participation = await ObtenirSelonId(id);
            SimulerVerifierStatus(participation);
            return participation.EstValide;
        }
        private void SimulerVerifierStatus(ParticipationDTO participation)
        {
            if (!participation.EstValide)
            {
                bool EstValide = new Random().Next(1, 10) > 5 ? true : false;
                participation.EstValide = EstValide;
            }
        }
    }
}
