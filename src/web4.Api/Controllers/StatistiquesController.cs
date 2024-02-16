using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatistiquesController : ControllerBase
    {
        // GET: api/<StatistiquesController>
        [HttpGet("/GetEvenementsProfitables")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }
        // GET: api/<StatistiquesController>
        [HttpGet("/GetVillesPopulaires")]
        public IEnumerable<string> GetVillesPopulaires()
        {
            return new string[] { "value1", "value2" };
        }
    }
}
