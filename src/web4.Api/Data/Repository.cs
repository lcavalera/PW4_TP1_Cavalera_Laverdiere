using Events.Api.Entites;

namespace Events.Api.Data
{
    public static class Repository
    {
        private static int _idSequenceEvenement = 1;
        private static int _idSequenceVille = 1;
        private static int _idSequenceParticipation = 1;
        public static ISet<Evenement> Evenements { get; set; } = new HashSet<Evenement>();
        public static ISet<Ville> Villes { get; set; } = new HashSet<Ville>();
        public static ISet<Participation> Participations { get; set; } = new HashSet<Participation>();


        public static Evenement AddEvenement(Evenement evenement)
        {

            evenement.Id = _idSequenceEvenement++;
            Evenements.Add(evenement);

            return evenement;
        }
        public static Ville AddVille(Ville ville)
        {

            ville.Id = _idSequenceVille++;
            Villes.Add(ville);

            return ville;
        }
        public static Participation AddParticipation(Participation participation)
        {

            participation.Id = _idSequenceParticipation++;
            Participations.Add(participation);

            return participation;
        }
    }
}
