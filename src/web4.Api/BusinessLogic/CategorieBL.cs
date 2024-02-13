using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class CategorieBL(IEvenementsBL evenementsBL, IAsyncRepository<Categorie> categorieRepo) : ICategorieBL
    {
        private readonly IEvenementsBL _evenementsBL = evenementsBL;
        private readonly IAsyncRepository<Categorie> _categorieRepo = categorieRepo;
        public async Task Ajouter(CategorieDTO categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            await _categorieRepo.AddAsync(new Categorie { Id = categorie.Id, Nom = categorie.Nom});
        }

        public async Task Modifier(int id, CategorieDTO categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            
            await _categorieRepo.EditAsync(new Categorie { Id = categorie.Id, Nom = categorie.Nom });
        }

        public async Task<CategorieDTO?> ObtenirSelonId(int id)
        {
            Categorie? categorie = await _categorieRepo.GetByIdAsync(id);
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = "Categorie introuvable" } };
            }
            return new CategorieDTO { Id = categorie.Id, Nom = categorie.Nom };
        }

        public async Task<List<CategorieDTO>> ObtenirTout()
        {
            IEnumerable<Categorie>? liste = await _categorieRepo.ListAsync();
            return liste.Select(c => new CategorieDTO { Id = c.Id, Nom = c.Nom }).ToList();
        }

        public async Task Supprimer(int id)
        {
            CategorieDTO? categorie = await ObtenirSelonId(id);
            var liste = await _evenementsBL.ObtenirTout();
            bool evenementAssocier = liste.Any(e => e.Categories.Any(c => c.Nom == categorie.Nom));
            if (evenementAssocier)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la categorie: un ou plusieurs évènement utilise cette catégorie " } };
            }
            await _categorieRepo.DeleteAsync(new Categorie { Id = categorie.Id, Nom = categorie.Nom});
        }
    }
}
