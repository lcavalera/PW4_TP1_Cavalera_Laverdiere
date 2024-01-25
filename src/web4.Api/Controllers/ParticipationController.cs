using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
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
        public ActionResult<Participation> GetById(int id)
        {
            Participation? participation = _participationBL.ObtenirSelonId(id);
            return participation == null ? NotFound() : Ok(participation);
        }

        // POST api/<ParticipationController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Participation> Post([FromBody] DemandeParticipation demandeParticipation)
        {
            Participation? participation = _participationBL.Ajouter(demandeParticipation);
            return CreatedAtAction(nameof(GetById), new { id = participation.Id }, null);
        }

        // PUT api/<ParticipationController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Participation participation)
        {
            _participationBL.Modifier(id, participation);
            return NoContent();
        }

        // DELETE api/<ParticipationController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _participationBL.Supprimer(id);
            return NoContent();
        }
        [HttpGet("{id}/status")]
        [ProducesResponseType(StatusCodes.Status303SeeOther)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Status(int id)
        {
            bool estValide = _participationBL.VerifierStatus(id);
            if (estValide)
            {
                Response.Headers.Add("Location", Url.Action(nameof(GetById), new { id }));
                return new StatusCodeResult(StatusCodes.Status303SeeOther);
            }
            return Ok(new { status = "demande en attente" });
        }
    }
}
