using System.ComponentModel.DataAnnotations;

namespace Events.Api.Entites
{
    public class Ville: BaseEntity
    {
        [Required]
        public string Nom { get; set; }
        public Region Region { get; set; }
        public List<Evenement>? Evenements { get; set; }
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
