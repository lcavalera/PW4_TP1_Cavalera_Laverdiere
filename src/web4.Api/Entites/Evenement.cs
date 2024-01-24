using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Evenement
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
        public List<Categorie> Categories { get; set; }
        [Required]
        public string Adresse { get; set; }
        [Required]
        public string NomOrganisateur { get; set; }
        [Required]
        public Ville Ville { get; set; }
        public int Prix { get; set; }

    }
}
