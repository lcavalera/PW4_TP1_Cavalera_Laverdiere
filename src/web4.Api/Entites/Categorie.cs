using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Categorie
    {
        public int Id { get; set; }
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
