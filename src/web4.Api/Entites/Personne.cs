using System.ComponentModel.DataAnnotations;

namespace web4.Api.Entites
{
    public abstract class Personne
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public string? Telephone { get; set; }
    }
}
