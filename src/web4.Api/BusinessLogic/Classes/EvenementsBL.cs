using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Events.Api.BusinessLogic.Classes
{
    public class EvenementsBL : IEvenementsBL
    {
        private readonly IAsyncRepositoryEvenements _evenementsRepository;
        private readonly IAsyncRepository<Categorie> _categoriesRepository;
        private readonly IAsyncRepository<Ville> _villesRepository;
        private readonly IMapper _mapper;

        public EvenementsBL(IAsyncRepositoryEvenements evenementsRepository, IAsyncRepository<Categorie> categoriesRepository, IAsyncRepository<Ville> villesRepository, IMapper mapper)
        {
            _evenementsRepository = evenementsRepository;
            _categoriesRepository = categoriesRepository;
            _villesRepository = villesRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<EvenementDTO>> ObtenirTout()
        {
            return _mapper.Map<List<EvenementDTO>>(await _evenementsRepository.ListAsync());
        }

        public async Task<EvenementDTO> ObtenirSelonId(int id)
        {
            Evenement evenement = await EvenementExiste(id);
            return _mapper.Map<EvenementDTO>(evenement);
        }

        public async Task<IEnumerable<EvenementDTO>> ObtenirSelonIdVille(int villeId)
        {
            IEnumerable<EvenementDTO> evenements = await ObtenirTout();
            return evenements.Where(p => p.VilleId == villeId).ToList();
        }

        public async Task Ajouter(EvenementDTO evenement)
        {
            await Validations(evenement);

            await _evenementsRepository.AddAsync(new Evenement
            {
                Id = evenement.Id,
                Titre = evenement.Titre,
                Description = evenement.Description,
                Adresse = evenement.Adresse,
                NomOrganisateur = evenement.NomOrganisateur,
                CategorieIds = evenement.CategorieIds,
                DateDebut = evenement.DateDebut,
                DateDeFin = evenement.DateDeFin,
                VilleId = evenement.VilleId,
                Prix = evenement.Prix,
            });
        }

        public async Task Modifier(int id, EvenementDTO evenement)
        {
            await Validations(evenement);

            Evenement evenementAmodifier = await EvenementExiste(id);

            evenementAmodifier.Id = evenement.Id;
            evenementAmodifier.Titre = evenement.Titre;
            evenementAmodifier.Description = evenement.Description;
            evenementAmodifier.Adresse = evenement.Adresse;
            evenementAmodifier.NomOrganisateur = evenement.NomOrganisateur;
            evenementAmodifier.CategorieIds = evenement.CategorieIds;
            evenementAmodifier.DateDebut = evenement.DateDebut;
            evenementAmodifier.DateDeFin = evenement.DateDeFin;
            evenementAmodifier.VilleId = evenement.VilleId;
            evenementAmodifier.Prix = evenement.Prix;

            await _evenementsRepository.EditAsync(evenementAmodifier);
        }

        public async Task Supprimer(int id)
        {
            Evenement evenement = await EvenementExiste(id);
            await _evenementsRepository.DeleteAsync(evenement);
        }

        private async Task Validations(EvenementDTO evenement)
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

            if (!await VilleExiste(evenement))
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (ville id={evenement.VilleId})" } };
            }

            if (!await PossiedeCategorie(evenement))
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Aucune categorie correspondante (évenement id={evenement.Id})" } };
            }
        }

        private async Task<Evenement> EvenementExiste(int id)
        {
            Evenement? evenement = await _evenementsRepository.GetByIdAsync(id);

            if (evenement == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return evenement;
        }

        private async Task<bool> VilleExiste(EvenementDTO evenement)
        {
            var villes = await _villesRepository.ListAsync();

            if (villes.Any(v => v.Id == evenement.VilleId))
            {
                return true;
            }
            return false;
        }

        private async Task<bool> PossiedeCategorie(EvenementDTO evenement)
        {
            foreach (int Id in evenement.CategorieIds.Select(c => c).ToList())
            {
                var categories = await _categoriesRepository.ListAsync();

                if (categories.Any(c => c.Id == Id))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
