using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Exceptions;
using System.Linq;

namespace Events.Api.BusinessLogic
{
    public class EvenementsBL: IEvenementsBL
    {
        private readonly IAsyncRepository<Evenement> _evenementsRepository;
        private readonly IAsyncRepository<Categorie> _categoriesRepository;
        private readonly IAsyncRepository<Ville> _villesRepository;

        public EvenementsBL(IAsyncRepository<Evenement> evenementsRepository, IAsyncRepository<Categorie> categoriesRepository, IAsyncRepository<Ville> villesRepository)
        {
            _evenementsRepository = evenementsRepository;
            _categoriesRepository = categoriesRepository;
            _villesRepository = villesRepository;
        }
    
        public async Task<IEnumerable<Evenement>> ObtenirTout()
        {
            return await _evenementsRepository.ListAsync();
        }
        public async Task<Evenement> ObtenirSelonId(int id)
        {
            var evenement = await _evenementsRepository.GetByIdAsync(id);
            
            if (evenement == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return evenement;
        }

        public async Task<IEnumerable<Evenement>> ObtenirSelonIdVille(int villeId)
        {
            var evenements = await _evenementsRepository.ListAsync();
            return evenements.Where(e => e.VilleId == villeId).ToList();
        }

        public async Task Ajouter(Evenement evenement)
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
            bool possedeCategorie = false;
            foreach (int Id in evenement.Categories.Select(c => c.Id))
            {
                var categories = await _categoriesRepository.ListAsync();

                if (categories.Any(c => c.Id == Id))
                {
                    possedeCategorie = true;
                }
            }
            if (!possedeCategorie)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Aucune categorie correspondante (évenement id={evenement.Id})" } };
            }

            if (!await VilleExhiste(evenement))
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (ville id={evenement.VilleId})" } };
            }

            await _evenementsRepository.AddAsync(evenement);
        }

        public async Task Modifier(int id, Evenement evenement)
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

            if (!await VilleExhiste(evenement))
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (ville id={evenement.VilleId})" } };
            }

            var evenementAModifier = await _evenementsRepository.GetByIdAsync(id);

            if (evenementAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            await _evenementsRepository.EditAsync(evenement);
        }

        public async Task Supprimer(int id)
        {
            var evenement = await _evenementsRepository.GetByIdAsync(id);

            if (evenement == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            else
            {
                await _evenementsRepository.DeleteAsync(evenement);
            }
        }

        private async Task<bool> VilleExhiste(Evenement evenement)
        {
            var villes = await _villesRepository.ListAsync();

            if (villes.Any(v => v.Id == evenement.VilleId))
            {
                return true;
            }
            return false;
        }
    }
}
