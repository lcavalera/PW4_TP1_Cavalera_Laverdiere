using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;
using System.Collections.Generic;

namespace Events.Api.BusinessLogic.Classes
{
    public class ParticipationBL(IAsyncParticipationRepository participationRepo, IMapper mapper) : IParticipationBL
    {
        private readonly IAsyncParticipationRepository _participationRepo = participationRepo;
        private readonly IMapper _mapper = mapper;
        public async Task Ajouter(ParticipationDTO demandeParticipation)
        {
            await Validations(demandeParticipation);

            await _participationRepo.AddAsync(new Participation
            {
                EstValide = false,
                Courriel = demandeParticipation.Courriel,
                EvenementID = demandeParticipation.EvenementID,
                Nom = demandeParticipation.Nom,
                Prenom = demandeParticipation.Prenom,
                NombrePlaces = demandeParticipation.NombrePlaces
            });
        }

        public async Task<List<ParticipationDTO>> ObtenirSelonEvenementId(int evenementId)
        {
            List<ParticipationDTO> participations = await ObtenirTout();
            return participations.Where(p => p.EvenementID == evenementId).ToList();
        }

        public async Task<ParticipationDTO?> ObtenirSelonId(int id)
        {
            Participation participation = await ParticipationExiste(id);
            return _mapper.Map<ParticipationDTO>(participation);
        }

        public async Task<List<ParticipationDTO>> ObtenirTout()
        {
            return _mapper.Map<List<ParticipationDTO>>(await _participationRepo.ListAsync());
        }

        public async Task Supprimer(int id)
        {
            Participation participation = await ParticipationExiste(id);
            await _participationRepo.DeleteAsync(participation);
        }

        public async Task<bool> VerifierStatus(int id)
        {
            var participation = _mapper.Map<ParticipationDTO>(await _participationRepo.GetByIdVerifyStatus(id));
            return participation is null
                ? throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } }
                : _participationRepo.SimulerVerifierStatus(await _participationRepo.GetByIdVerifyStatus(id));
        }
        private async Task Validations(ParticipationDTO demandeParticipation)
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
            IEnumerable<Participation>? participations = await _participationRepo.ListAsync();

            bool participeDeja = participations.Any(p => p.Courriel == demandeParticipation.Courriel && p.EvenementID == demandeParticipation.EvenementID);
            if (participeDeja)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Cette adresse électronique participe déjà à cet Évènement" } };
            }
        }

        private async Task<Participation> ParticipationExiste(int id)
        {
            Participation? participation = await _participationRepo.GetByIdAsync(id);

            if (participation == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return participation;
        }
    }
}
