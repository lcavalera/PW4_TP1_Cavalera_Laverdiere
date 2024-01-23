using web4.Api.Entites;

namespace web4.Api.Data
{
    public static class Repository
    {
        private static int _ID_SEQUENCE_PRODUCT { get; set; } = 1;
        public static ISet<Personne> Personnes { get; set; } = new HashSet<Personne>();
        public static ISet<Evenement> Evenements { get; set; } = new HashSet<Evenement>();
        public static ISet<Ville> Villes { get; set; } = new HashSet<Ville>();


        public static Personne AddPersonne(Personne personne)
        {

            personne.Id = _ID_SEQUENCE_PRODUCT++;
            Personnes.Add(personne);

            return personne;
        }
        public static Evenement AddEvenement(Evenement evenement)
        {

            evenement.Id = _ID_SEQUENCE_PRODUCT++;
            Evenements.Add(evenement);

            return evenement;
        }
        public static Ville AddVille(Ville ville)
        {

            ville.Id = _ID_SEQUENCE_PRODUCT++;
            Villes.Add(ville);

            return ville;
        }
    }
}
