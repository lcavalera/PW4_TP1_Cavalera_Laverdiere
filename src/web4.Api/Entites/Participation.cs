using System.ComponentModel.DataAnnotations;

namespace web4.Api.Entites
{
    public class Participation
    {
        public int Id { get; set; }
        [Required]
        public int NombrePlaces { get; set; }
        [EmailAddress, Required]
        public string Courriel { get; set; }
        [Required]
        public string Nom {  get; set; }
        [Required]
        public string Prenom {  get; set; }

        //À valider à la place de "Nom", "Prenom" et "Courriel":

        //public int UsagerId { get; set; }
    }
}
