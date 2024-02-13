using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites.DTO
{
    public class VilleDTO: BaseEntity
    {
        [Required]
        public string Nom { get; set; }
        public Region Region { get; set; }

    }
}
