using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic.Classes
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
            //IEnumerable<Ville>? villes = await _villesRepository.ListAsync();
            //return villes.Select(v => new VilleDTO { Id=v.Id, Nom=v.Nom, Region=v.Region }).ToList();

            return _mapper.Map<List<VilleDTO>>(await _villesRepository.ListAsync());
        }

        public async Task<VilleDTO> ObtenirSelonId(int id)
        {
            return _mapper.Map<VilleDTO>(await _villesRepository.GetByIdAsync(id)) ?? throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } }; ;
        }

        public async Task Ajouter(VilleDTO ville)
        {
            await Validations(ville);

            await _villesRepository.AddAsync(new Ville
            {
                Id = ville.Id,
                Nom = ville.Nom,
                Region = ville.Region
            });
        }

        public async Task Modifier(int id, VilleDTO ville)
        {
            await Validations(ville);

            Ville villeAModifier = await VilleExiste(id);

            villeAModifier.Id = ville.Id;
            villeAModifier.Nom = ville.Nom;
            villeAModifier.Region = ville.Region;

            await _villesRepository.EditAsync(villeAModifier);

        }

        public async Task Supprimer(int id)
        {
            Ville ville = await VilleExiste(id);

            await EvenementExiste(ville);

            await _villesRepository.DeleteAsync(ville);
        }

        private async Task Validations(VilleDTO ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
        }

        private async Task<Ville> VilleExiste(int id)
        {
            Ville ville = await _villesRepository.GetByIdAsync(id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return ville;
        }

        private async Task EvenementExiste(Ville ville)
        {
            var evenements = await _evenementsRepository.ListAsync();

            if (evenements.Any(e => e.VilleId == ville.Id))
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la ville: un ou plusieurs évènement utilise cette ville" } };
            }
        }
    }
}
