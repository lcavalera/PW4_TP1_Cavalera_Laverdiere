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

        /// <summary>
        /// Retourne une liste des évènements en ordre de rentabilité
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/evenements
        ///
        /// </remarks>
        /// <response code="200">évènements trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<StatistiquesController>
        [HttpGet("/GetEvenementsProfitables")]
        [ProducesResponseType(typeof(List<EvenementsProfitablesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public Task<List<EvenementsProfitablesDTO>> Get()
        {
            return _statistiquesBL.ObtenirEvenementsProfitables();
        }

        /// <summary>
        /// Retourne une liste des villes le plus populaires en ordre de nombre des évènements
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/villes
        ///
        /// </remarks>
        /// <response code="200">villes trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<StatistiquesController>
        [HttpGet("/GetVillesPopulaires")]
        [ProducesResponseType(typeof(List<VillesPopulairesDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<List<VillesPopulairesDTO>> GetVillesPopulaires()
        {
            return await _statistiquesBL.ObtenirVillesPopulairesAsync();
        }
    }
}
