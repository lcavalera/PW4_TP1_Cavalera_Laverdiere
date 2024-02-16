using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites.DTO
{
    public class EvenementDTO
    {
        public int Id { get; set; }
        [Required]
        public DateTime DateDebut { get; set; }
        [Required]
        public DateTime DateDeFin { get; set; }
        [Required]
        public string Titre { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string NomOrganisateur { get; set; }
        public int Prix { get; set; }

        [Required]
        public List<int> CategorieIds { get; set; }

        [Required]
        public int VilleId { get; set; }
    }
}
