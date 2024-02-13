using AutoMapper;
using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class VillesBL : IVillesBL
    {
        private readonly IAsyncRepository<Ville> _villesRepository;
        private readonly IAsyncRepository<Evenement> _evenementsRepository;
        private readonly IMapper _mapper;

        public VillesBL(IAsyncRepository<Ville> villesRepository, IAsyncRepository<Evenement> evenementsRepository, IMapper mapper)
        {
            _villesRepository = villesRepository;
            _evenementsRepository = evenementsRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<VilleDTO>> ObtenirTout()
        {
            IEnumerable<Ville>? villes = await _villesRepository.ListAsync();
            return villes.Select(v => new VilleDTO { Id=v.Id, Nom=v.Nom, Region=v.Region }).ToList();
        }

        public async Task<VilleDTO> ObtenirSelonId(int id)
        {
            Ville ville = await _villesRepository.GetByIdAsync(id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return new VilleDTO { Id = ville.Id, Nom = ville.Nom, Region = ville.Region };
        }

        public async Task Ajouter(VilleDTO ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            await _villesRepository.AddAsync(new Ville { Id = ville.Id, Nom = ville.Nom, Region = ville.Region });
        }

        public async Task Modifier(int id, VilleDTO ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            Ville villeAModifier = await _villesRepository.GetByIdAsync(id);

            if (villeAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            await _villesRepository.EditAsync(new Ville { Id = ville.Id, Nom = ville.Nom, Region = ville.Region });
        }
        public async Task Supprimer(int id)
        {
            Ville ville = await _villesRepository.GetByIdAsync(id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                var evenements = await _evenementsRepository.ListAsync();

                if (evenements.Any(e => e.VilleID == ville.Id))
                {
                    throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la ville: un ou plusieurs évènement utilise cette ville" } };
                }

                await _villesRepository.DeleteAsync(ville);
            }
        }
    }
}
