using Events.Api.Entites;

namespace Events.Api.Data
{
    public static class Repository
    {
        private static int IdSequencePersonne = 1;
        private static int IdSequenceEvenement = 1;
        private static int IdSequenceVille = 1;
        private static int IdSequenceParticipation = 1;
        public static ISet<Personne> Personnes { get; set; } = new HashSet<Personne>();
        public static ISet<Evenement> Evenements { get; set; } = new HashSet<Evenement>();
        public static ISet<Ville> Villes { get; set; } = new HashSet<Ville>();
        public static ISet<Participation> Participations { get; set; } = new HashSet<Participation>();


        public static Personne AddPersonne(Personne personne)
        {

            personne.Id = IdSequencePersonne++;
            Personnes.Add(personne);

            return personne;
        }
        public static Evenement AddEvenement(Evenement evenement)
        {

            evenement.Id = IdSequenceEvenement++;
            Evenements.Add(evenement);

            return evenement;
        }
        public static Ville AddVille(Ville ville)
        {

            ville.Id = IdSequenceVille++;
            Villes.Add(ville);

            return ville;
        }
        public static Participation AddParticipation(Participation participation)
        {

            participation.Id = IdSequenceParticipation++;
            Participations.Add(participation);

            return participation;
        }
    }
}
