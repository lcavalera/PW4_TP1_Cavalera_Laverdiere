using System.ComponentModel.DataAnnotations;

namespace web4.Api.Entites
{
    public class Usager: Personne
    {
        [Required]
        public string Prenom { get; set; }
        public DateTime? DateNaissance { get; set; }
        [EmailAddress, Required]
        public string Courriel { get; set; }
    }
}
