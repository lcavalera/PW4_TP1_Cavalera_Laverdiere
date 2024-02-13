using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
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
    
        public async Task<IEnumerable<EvenementDTO>> ObtenirTout()
        {
            IEnumerable<Evenement>? evenements = await _evenementsRepository.ListAsync();
            return evenements.Select(e => new EvenementDTO { Id=e.Id, Titre=e.Titre, Description=e.Description, Adresse=e.Adresse, NomOrganisateur=e.NomOrganisateur, Categories=e.Categories, DateDebut=e.DateDebut, DateDeFin=e.DateDeFin, Ville=e.Ville, Prix=e.Prix}).ToList();
        }
        public async Task<EvenementDTO> ObtenirSelonId(int id)
        {
            Evenement evenement = await _evenementsRepository.GetByIdAsync(id);
            
            if (evenement == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return new EvenementDTO { Id = evenement.Id, Titre = evenement.Titre, Description = evenement.Description, Adresse = evenement.Adresse, NomOrganisateur = evenement.NomOrganisateur, Categories = evenement.Categories, DateDebut = evenement.DateDebut, DateDeFin = evenement.DateDeFin, Ville = evenement.Ville, Prix = evenement.Prix };
        }

        public async Task<IEnumerable<EvenementDTO>> ObtenirSelonIdVille(int villeId)
        {
            IEnumerable<Evenement>? evenements = await _evenementsRepository.ListAsync();
            return evenements.Where(p => p.VilleID == villeId).Select(e => new EvenementDTO { Id = e.Id, Titre = e.Titre, Description = e.Description, Adresse = e.Adresse, NomOrganisateur = e.NomOrganisateur, Categories = e.Categories, DateDebut = e.DateDebut, DateDeFin = e.DateDeFin, Ville = e.Ville, Prix = e.Prix }).ToList();
        }

        public async Task Ajouter(EvenementDTO evenement)
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
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (ville id={evenement.VilleID})" } };
            }

            await _evenementsRepository.AddAsync(new Evenement {
                Id = evenement.Id,
                Titre = evenement.Titre,
                Description = evenement.Description,
                Adresse = evenement.Adresse,
                NomOrganisateur = evenement.NomOrganisateur,
                Categories = evenement.Categories,
                DateDebut = evenement.DateDebut,
                DateDeFin = evenement.DateDeFin,
                Ville = evenement.Ville,
                Prix = evenement.Prix
            });
        }

        public async Task Modifier(int id, EvenementDTO evenement)
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
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (ville id={evenement.VilleID})" } };
            }

            var evenementAModifier = await _evenementsRepository.GetByIdAsync(id);

            if (evenementAModifier == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }
            await _evenementsRepository.EditAsync(new Evenement
            {
                Id = evenement.Id,
                Titre = evenement.Titre,
                Description = evenement.Description,
                Adresse = evenement.Adresse,
                NomOrganisateur = evenement.NomOrganisateur,
                Categories = evenement.Categories,
                DateDebut = evenement.DateDebut,
                DateDeFin = evenement.DateDeFin,
                Ville = evenement.Ville,
                Prix = evenement.Prix
            });
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

        private async Task<bool> VilleExhiste(EvenementDTO evenement)
        {
            var villes = await _villesRepository.ListAsync();

            if (villes.Any(v => v.Id == evenement.VilleID))
            {
                return true;
            }
            return false;
        }
    }
}
