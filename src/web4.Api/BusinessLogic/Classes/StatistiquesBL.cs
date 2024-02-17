using AutoMapper;
using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data.Interfaces;
using Events.Api.Entites.DTO;

namespace Events.Api.BusinessLogic.Classes
{
    public class StatistiquesBL(IAsyncRepositoryVilles villesRepo, IAsyncRepositoryEvenements evenementsRepo, IMapper mapper) : IStatistiquesBL
    {
        private readonly IAsyncRepositoryVilles _villesRepo = villesRepo;
        private readonly IAsyncRepositoryEvenements _evenementsRepo = evenementsRepo;
        private readonly IMapper _mapper = mapper;
        public async Task<List<string>> ObtenirVillesPopulairesAsync()
        {
            return await _villesRepo.GetVillesPopulairesAsync();
        }
        public async Task<List<EvenementsProfitablesDTO>> ObtenirEvenementsProfitables()
        {
            var events = _mapper.Map<List<EvenementDTO>>(await _evenementsRepo.ListAsync());
            List<EvenementsProfitablesDTO> liste = [];
            foreach (var e in events)
            {
                liste.Add(new EvenementsProfitablesDTO { Nom = e.Titre, Profit = await _evenementsRepo.GetTotal(e.Id)});
            }
            return liste.OrderByDescending(e => e.Profit).Take(10).ToList();
        }
    }
}
