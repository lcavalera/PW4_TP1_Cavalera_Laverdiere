using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Categorie: BaseEntity
    {
        [Required]
        public NomCategorie Nom { get; set; }
    }

    public enum NomCategorie
    {
        Spectacle,
        Theatre,
        Festival,
        Musique,
        Sport
    }
}
