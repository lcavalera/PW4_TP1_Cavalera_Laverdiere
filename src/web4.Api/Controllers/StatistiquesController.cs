using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Entites.DTO;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiquesController(IStatistiquesBL statistiquesBL) : ControllerBase
    {
        private readonly IStatistiquesBL _statistiquesBL = statistiquesBL;
        // GET: api/<StatistiquesController>
        [HttpGet("/GetEvenementsProfitables")]
        public Task<List<EvenementsProfitablesDTO>> Get()
        {
            return _statistiquesBL.ObtenirEvenementsProfitables();
        }
        // GET: api/<StatistiquesController>
        [HttpGet("/GetVillesPopulaires")]
        public async Task<List<string>> GetVillesPopulaires()
        {
            return await _statistiquesBL.ObtenirVillesPopulairesAsync();
        }
    }
}
