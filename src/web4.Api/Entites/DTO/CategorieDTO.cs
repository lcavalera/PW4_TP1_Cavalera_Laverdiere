using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites.DTO
{
    public class CategorieDTO : BaseEntity
    {
        [Required]
        public string Nom { get; set; }
    }
}
