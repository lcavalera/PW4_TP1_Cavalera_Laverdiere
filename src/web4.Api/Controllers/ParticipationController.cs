using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ParticipationController(IParticipationBL participationBL) : ControllerBase
    {
        private readonly IParticipationBL _participationBL = participationBL;

        // GET: api/<ParticipationController>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Participation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Participation>> Get()
        {
            return Ok(_participationBL.ObtenirTout());
        }

        // GET api/<ParticipationController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Participation> Get(int id)
        {
            Participation? participation = _participationBL.ObtenirSelonId(id);
            return participation == null ? NotFound() : Ok(participation);
        }

        // POST api/<ParticipationController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ParticipationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ParticipationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
