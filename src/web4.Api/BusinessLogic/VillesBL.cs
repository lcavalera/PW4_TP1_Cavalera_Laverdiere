using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class VillesBL : IVillesBL
    {
        private readonly IAsyncRepository<Ville> _villesRepository;
        private readonly IAsyncRepository<Evenement> _evenementsRepository;

        public VillesBL(IAsyncRepository<Ville> villesRepository, IAsyncRepository<Evenement> evenementsRepository)
        {
            _villesRepository = villesRepository;
            _evenementsRepository = evenementsRepository;
        }

        public async Task<IEnumerable<Ville>> ObtenirTout()
        {
            return await _villesRepository.ListAsync();
        }
        public async Task<Ville> ObtenirSelonId(int id)
        {
            var ville = await _villesRepository.GetByIdAsync(id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return ville;
        }
        public async Task Ajouter(Ville ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            await _villesRepository.AddAsync(ville);
        }
        public async Task Modifier(int id, Ville ville)
        {
            if (ville == null)
            {
                //BadRequest
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            var villeAModifier = await _villesRepository.GetByIdAsync(id);

            if (villeAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            await _villesRepository.EditAsync(ville);
        }
        public async Task Supprimer(int id)
        {
            var ville = await _villesRepository.GetByIdAsync(id);

            if (ville == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                var evenements = await _evenementsRepository.ListAsync();

                if (evenements.Any(e => e.VilleId == ville.Id))
                {
                    throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la ville: un ou plusieurs évènement utilise cette ville" } };
                }

                await _villesRepository.DeleteAsync(ville);
            }
        }
    }
}
