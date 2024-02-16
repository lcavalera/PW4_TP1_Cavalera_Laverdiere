using AutoMapper;
using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;
using System.Collections.Generic;

namespace Events.Api.BusinessLogic
{
    public class ParticipationBL(IAsyncParticipationRepository participationRepo, IMapper mapper) : IParticipationBL
    {
        private readonly IAsyncParticipationRepository _participationRepo = participationRepo;
        private readonly IMapper _mapper = mapper;
        public async Task Ajouter(ParticipationDTO demandeParticipation)
        {
            await Validations(demandeParticipation);

            // toute demande ajouté est invalide,    #6 de l`énoncé
            demandeParticipation.EstValide = false;

            await _participationRepo.AddAsync(_mapper.Map<Participation>(demandeParticipation));
        }

        public async Task<List<ParticipationDTO>> ObtenirSelonEvenementId(int evenementId)
        {
            List<ParticipationDTO> participations = await ObtenirTout();
            return participations.Where(p => p.EvenementID == evenementId).ToList();
        }

        public async Task<ParticipationDTO?> ObtenirSelonId(int id)
        {
            return _mapper.Map<ParticipationDTO>(await _participationRepo.GetByIdAsync(id)) ?? throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
        }

        public async Task<List<ParticipationDTO>> ObtenirTout()
        {
            //var liste = _mapper.Map<List<ParticipationDTO>>(await _participationRepo.ListAsync());
            //return liste.Where(l => l.EstValide).ToList();
            return _mapper.Map<List<ParticipationDTO>>(await _participationRepo.ListAsync());
        }

        public async Task Supprimer(int id)
        {
            await _participationRepo.DeleteAsync(_mapper.Map<Participation>(await ObtenirSelonId(id)));
        }
        public async Task<bool> VerifierStatus(int id)
        {
            var participation = _mapper.Map<ParticipationDTO>(await _participationRepo.GetByIdVerifyStatus(id));
            if (participation is null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            return SimulerVerifierStatus(participation);
        }
        private bool SimulerVerifierStatus(ParticipationDTO participation)
        {
            if (!participation.EstValide)
            {
                participation.EstValide = new Random().Next(1, 10) > 5 ? true : false;
            }
            return participation.EstValide;
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
    }
}
