using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations.@Schema;
using Swashbuckle.AspNetCore.Annotations;
using Events.Api.Filters.Swagger;
namespace Events.Api.Entites.DTO
{
    public class ParticipationDTO : BaseEntity
    {
        [Required]
        public int NombrePlaces { get; set; }
        [EmailAddress, Required]
        public string Courriel { get; set; }
        [Required]
        public string Nom { get; set; }
        [Required]
        public string Prenom { get; set; }
        [Required]
        public int EvenementID { get; set; }
    }
}
