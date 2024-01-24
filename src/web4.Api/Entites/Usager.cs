using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Usager
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        public string? Telephone { get; set; }
        public DateTime? DateNaissance { get; set; }
        [EmailAddress, Required]
        public string Courriel { get; set; }
    }
}
