using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites.DTO;
using Microsoft.Extensions.Logging;

namespace Events.Api.BusinessLogic.Classes
{
    public class StatistiquesBL(IAsyncRepositoryVilles villesRepo, IAsyncRepositoryEvenements evenementsRepo) : IStatistiquesBL
    {
        private readonly IAsyncRepositoryVilles _villesRepo = villesRepo;
        private readonly IAsyncRepositoryEvenements _evenementsRepo = evenementsRepo;
        public async Task<List<VillesPopulairesDTO>> ObtenirVillesPopulairesAsync()
        {
            var villes = await _villesRepo.GetVillesEvenementsAsync();

            List<VillesPopulairesDTO> liste = [];
            foreach (var v in villes)
            {
                liste.Add(new VillesPopulairesDTO { Nom = v.Nom, NbEvenements = v.Evenements.Count() });
            }

            return liste.OrderByDescending(v=> v.NbEvenements).Take(10).ToList();
        }
        public async Task<List<EvenementsProfitablesDTO>> ObtenirEvenementsProfitables()
        {
            var events = await _evenementsRepo.ListAsync();

            List<EvenementsProfitablesDTO> liste = [];
            foreach (var e in events)
            {
                liste.Add(new EvenementsProfitablesDTO { Nom = e.Titre, Profit = await _evenementsRepo.GetTotal(e.Id)});
            }
            return liste.OrderByDescending(e => e.Profit).Take(10).ToList();
        }
    }
}
