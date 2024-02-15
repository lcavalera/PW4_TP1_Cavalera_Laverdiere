using Events.Api.BusinessLogic;
using Events.Api.Entites;
using System.Diagnostics;

namespace Events.Api.Data
{
    public static class DbInitializer
    {
        public static void Initialize(EventsContext context)
        {
            //On valide s'il y a des données dans la BD
            if (context.Categories.Any() || context.Villes.Any() || context.Participations.Any() || context.Evenements.Any())
            {
                return;
            }

            var villes = new Ville[]
            {
                new Ville { Nom="Québec", Region=Region.CapitaleNationale },
                new Ville { Nom="Montreal", Region=Region.Montréal },
                new Ville { Nom="Rivière-du-Loup", Region=Region.BasSaintLaurent},
                new Ville { Nom="Sherbrooke", Region=Region.Estrie}
            };

            context.Villes.AddRange(villes);

            var categories = new Categorie[]
{
                new Categorie { Nom="Sport" },
                new Categorie { Nom="Spectacle"},
                new Categorie { Nom="Musique" },
                new Categorie { Nom="Festival" },
                new Categorie { Nom="Theatre" }
};

            context.Categories.AddRange(categories);

            var evenements = new Evenement[]
{
                new Evenement { Titre="Pentathlon des neiges", Description="Événement de plein air hivernal", Adresse="Plaines d'Abraham", DateDebut=new DateTime(2024,02,17), DateDeFin=new DateTime(2024,02,25), NomOrganisateur ="Ville de Québec", CategorieIds=[ 0, 3 ], Ville=villes[0], Prix=0},
                new Evenement { Titre="ONF Live in Canada", Description="Concert", Adresse="Rialto Theatre, 5723 Avenue du Parc", DateDebut=new DateTime(2024,03,03), DateDeFin=new DateTime(2024,03,03), NomOrganisateur ="J&B Entertainment", CategorieIds=[ 1, 2 ], Ville=villes[1], Prix=60},
                new Evenement { Titre="Ingrid St‑Pierre", Description="Concert seul au piano", Adresse="Grand Théâtre de Québec, 269 Bd René-Lévesque E", DateDebut=new DateTime(2024,02,17), DateDeFin=new DateTime(2024,02,20), NomOrganisateur ="Grand Théâtre Québec", CategorieIds=[ 2 ], Ville=villes[0], Prix=50},
                new Evenement { Titre="Great Ice! Sled Dog Rides", Description="Promenades en chiens de traîneau", Adresse="Shore Acres, 237 Shore Acres Drive North Hero", DateDebut=new DateTime(2024,02,16), DateDeFin=new DateTime(2024,02,16), NomOrganisateur ="Great Ice", CategorieIds=[ 0, 3 ], Ville=villes[3], Prix=13},
                new Evenement { Titre="Basketball - Concordia vs UQAM", Description="Dernier match à domicile à la saison régulière!", Adresse="Centre sportif de l'UQAM, 1212 rue Sanguinet", DateDebut=new DateTime(2024,02,24), DateDeFin=new DateTime(2024,02,24), NomOrganisateur ="Citadins de l'UQAM", CategorieIds=[ 0 ], Ville=villes[1]},
                new Evenement { Titre="Billy Tellier", Description="Souper spectacle d'humour", Adresse="Centre Maillet, 12 Ben-Martin Avenue", DateDebut=new DateTime(2024,05,03), DateDeFin=new DateTime(2024,05,05), NomOrganisateur ="Levée de fonds", CategorieIds=[ 2, 4 ], Ville=villes[2]}
};

            context.Evenements.AddRange(evenements);

            var participations = new Participation[]
            {
                new Participation { Nom= "Peggy", Prenom="Justice", Courriel="jpeggy76@gmail.com", NombrePlaces=2, Evenement=evenements[0], EstValide=true},
                new Participation { Nom= "Norman", Prenom="Laura", Courriel="lnorman546@outlook.com", NombrePlaces=3, Evenement=evenements[3], EstValide=true},
                new Participation { Nom= "Larabi", Prenom="Samuel", Courriel="larabi_sam@gmail.com", NombrePlaces=3, Evenement=evenements[1], EstValide=true},
                new Participation { Nom= "Anand", Prenom="Arturo", Courriel="art_anaud@hotmail.com", NombrePlaces=4, Evenement=evenements[2], EstValide=true},
                new Participation { Nom= "Yan", Prenom="Li", Courriel="liyan87@gmail.com", NombrePlaces=1, Evenement=evenements[4], EstValide=true},
                new Participation { Nom= "Powel", Prenom="Simon", Courriel="spowel678@gmail.com", NombrePlaces=1, Evenement=evenements[5], EstValide=false},
                new Participation { Nom= "Koll", Prenom="Martin", Courriel="mart347@outlook.com", NombrePlaces=4, Evenement=evenements[2], EstValide=true},
                new Participation { Nom= "Nino", Prenom="Olivetto", Courriel="nolivetto47@gmail.com", NombrePlaces=2, Evenement=evenements[0], EstValide=true}
            };

            context.Participations.AddRange(participations);

            context.SaveChanges();
        }
    }
}
