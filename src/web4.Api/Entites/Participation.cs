using Events.Api.Filters.Swagger;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Api.Entites
{
    public class Participation: BaseEntity
    {
        [Required]
        public int NombrePlaces { get; set; }
        [EmailAddress, Required]
        public string Courriel { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [SwaggerIgnore]
        public bool EstValide { get; set; }

        [Required]
        public int EvenementID { get; set; }
        public virtual Evenement? Evenement { get; set; }

    }
}
