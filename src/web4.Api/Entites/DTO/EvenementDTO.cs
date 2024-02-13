using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites.DTO
{
    public class EvenementDTO: BaseEntity
    {
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
        public int? Prix { get; set; }

        [Required]
        public ICollection<Categorie> Categories { get; set; }

        [Required]
        public int VilleID { get; set; }
        public virtual Ville? Ville { get; set; }
    }
}
