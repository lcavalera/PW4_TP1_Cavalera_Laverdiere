using AutoMapper;
using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
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
            await _categorieRepo.AddAsync(_mapper.Map<Categorie>(categorie));
        }
        public async Task Modifier(int id, CategorieDTO categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            
            await _categorieRepo.EditAsync(_mapper.Map<Categorie>(categorie));
        }

        public async Task<CategorieDTO?> ObtenirSelonId(int id)
        {
            return _mapper.Map<CategorieDTO>(await _categorieRepo.GetByIdAsync(id)) ?? throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = "Categorie introuvable" } };
        }

        public async Task<List<CategorieDTO>> ObtenirTout()
        {
            return _mapper.Map<List<CategorieDTO>>(await _categorieRepo.ListAsync());
        }

        public async Task Supprimer(int id)
        {
            CategorieDTO? categorie = await ObtenirSelonId(id);
            var liste = await _evenementsBL.ObtenirTout(); ////////////////////////////////////////////// a changer apres le passage devenement en dto        //////////////////////////////////////////////////////////////////////////
            bool evenementAssocier = liste.Any(e => e.CategoriesIds.Any(c => c == categorie.Id));
            if (evenementAssocier)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la categorie: un ou plusieurs évènement utilise cette catégorie " } };
            }
            await _categorieRepo.DeleteAsync(_mapper.Map<Categorie>(categorie));
        }
    }
}
