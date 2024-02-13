using System.ComponentModel.DataAnnotations;

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
        public bool EstValide { get; set; }

        [Required]
        public int EvenementID { get; set; }
        //public Evenement? Evenement { get; set; }

    }
}
