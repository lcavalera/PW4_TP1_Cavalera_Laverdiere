using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic.Classes
{
    public class CategorieBL(IEvenementsBL evenementsBL, IAsyncRepository<Categorie> categorieRepo, IMapper mapper) : ICategorieBL
    {
        private readonly IEvenementsBL _evenementsBL = evenementsBL;
        private readonly IAsyncRepository<Categorie> _categorieRepo = categorieRepo;
        private readonly IMapper _mapper = mapper;
        public async Task Ajouter(CategorieDTO categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            await _categorieRepo.AddAsync(new Categorie
            {
                Nom = categorie.Nom
            });
        }
        public async Task Modifier(int id, CategorieDTO categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }

            Categorie categorieAmodifier = await CategorieExiste(id);

            categorieAmodifier.Nom = categorie.Nom;

            await _categorieRepo.EditAsync(categorieAmodifier);
        }

        public async Task<CategorieDTO?> ObtenirSelonId(int id)
        {
            Categorie categorie = await CategorieExiste(id);

            return _mapper.Map<CategorieDTO>(categorie);
        }

        public async Task<List<CategorieDTO>> ObtenirTout()
        {
            return _mapper.Map<List<CategorieDTO>>(await _categorieRepo.ListAsync());
        }

        public async Task Supprimer(int id)
        {
            Categorie categorie = await CategorieExiste(id);
            var liste = await _evenementsBL.ObtenirTout();
            bool evenementAssocier = liste.Any(e => e.CategorieIds.Any(c => c == categorie.Id));
            if (evenementAssocier)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la categorie: un ou plusieurs évènement utilise cette catégorie " } };
            }
            await _categorieRepo.DeleteAsync(categorie);
        }
        private async Task<Categorie> CategorieExiste(int id)
        {
            Categorie? categorie = await _categorieRepo.GetByIdAsync(id);

            if (categorie == null)
            {
                //NotFound
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = $"Element introuvable (id={id})" } };
            }

            return categorie;
        }

    }
}
