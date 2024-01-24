using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Ville
    {
        public int Id { get; set; }
        [Required]
        public string Nom { get; set; }
        public Region Region { get; set; }
    }

    public enum Region
    {
        BasSaintLaurent,
        CapitaleNationale,
        Estrie,
        Montréal,
        Laurentides,
        Montérégie
    }
}
