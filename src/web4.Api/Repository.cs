using Events.Api.Entites;

namespace Events.Api
{
    public static class Repository
    {
        private static int _idSequenceVille { get; set; } = 1;
        private static int _idSequenceUsager { get; set; } = 1;
        private static int _idSequenceEvenement { get; set; } = 1;

        public static ISet<Ville> Villes { get; set; } = new HashSet<Ville>();
        public static ISet<Usager> Usagers { get; set; } = new HashSet<Usager>();
        public static ISet<Evenement> Evenements { get; set; } = new HashSet<Evenement>();

        public static Ville AddVille(Ville ville)
        {
            ville.Id = _idSequenceVille++;
            Villes.Add(ville);
            return ville;
        }

        public static Usager AddUsager(Usager usager)
        {
            usager.Id = _idSequenceUsager++;
            Usagers.Add(usager);
            return usager;
        }
        public static Evenement AddEvenement(Evenement evenement)
        {
            evenement.Id = _idSequenceEvenement++;
            Evenements.Add(evenement);
            return evenement;
        }
    }
}
