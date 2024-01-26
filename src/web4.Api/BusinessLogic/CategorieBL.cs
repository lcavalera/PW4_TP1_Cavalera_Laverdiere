using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Exceptions;

namespace Events.Api.BusinessLogic
{
    public class CategorieBL(IEvenementsBL evenementsBL) : ICategorieBL
    {
        private readonly IEvenementsBL _evenementsBL = evenementsBL;
        public Categorie Ajouter(Categorie categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            return Repository.AddCategorie(categorie);
        }

        public void Modifier(int id, Categorie categorie)
        {
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Parametres d'entrés non valides" } };
            }
            Categorie? categorieAModifier = ObtenirSelonId(id);
            if (categorieAModifier == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = "Categorie a modifier introuvable" } };
            }
            categorieAModifier.Nom = categorie.Nom;
        }

        public Categorie? ObtenirSelonId(int id)
        {
            Categorie? categorie = Repository.Categories.FirstOrDefault(c => c.Id == id);
            if (categorie == null)
            {
                throw new HttpException { StatusCode = StatusCodes.Status404NotFound, Errors = new { Errors = "Categorie introuvable" } };
            }
            return categorie;
        }

        public List<Categorie> ObtenirTout()
        {
            return Repository.Categories.ToList();
        }

        public void Supprimer(int id)
        {
            Categorie? categorie = ObtenirSelonId(id);
            bool evenementAssocier = _evenementsBL.ObtenirTout().Any(e => e.Categories.Any(c => c.Nom == categorie.Nom));
            if (evenementAssocier)
            {
                throw new HttpException { StatusCode = StatusCodes.Status400BadRequest, Errors = new { Errors = "Impossible de supprimer la categorie: un ou plusieurs évènement utilise cette catégorie " } };
            }
            Repository.Categories.Remove(categorie);
        }
    }
}
