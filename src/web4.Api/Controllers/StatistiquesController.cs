using Events.Api.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiquesController(IVillesBL villesBL) : ControllerBase
    {
        private readonly IVillesBL _villesBL = villesBL;
        // GET: api/<StatistiquesController>
        [HttpGet("/GetEvenementsProfitables")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/<StatistiquesController>
        [HttpGet("/GetVillesPopulaires")]
        public async Task<List<string>> GetVillesPopulaires()
        {
            return await _villesBL.ObtenirVillesPopulaires();
        }
        //public async Task GetVillesPopulaires()
        //{
        //    var test = await _context.Set<Ville>().Include(v => v.Evenements).ToListAsync();
        //    var meh = test.OrderByDescending(c => c.Evenements.Count);
        //    var y = meh.Select(c => c.Nom).Take(10).ToList();
        //    var e = y;
        //}
    }
}
