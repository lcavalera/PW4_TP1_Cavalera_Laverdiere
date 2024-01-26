using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ParticipationController(IParticipationBL participationBL) : ControllerBase
    {
        private readonly IParticipationBL _participationBL = participationBL;


        /// <summary>
        /// Retourne une liste des participations 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/participations
        ///
        /// </remarks>
        /// <response code="200">participations trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<ParticipationController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Participation>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Participation>> Get()
        {
            return Ok(_participationBL.ObtenirTout());
        }
        /// <summary>
        /// Retourne une participation spécifique à partir de son id
        /// </summary>
        /// <param name="id">id de la participation à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Participation/1
        ///
        /// </remarks>
        /// <response code="200">participation trouvé et retourné</response>
        /// <response code="404">participation introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<ParticipationController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Participation> GetById(int id)
        {
            Participation? participation = _participationBL.ObtenirSelonId(id);
            return participation == null ? NotFound() : Ok(participation);
        }
        /// <summary>
        /// Ajoute une participation à la base de donnée
        /// </summary>
        /// <param name="demandeParticipation">demande de participation à ajouter</param>
        /// <returns>Une nouvelle participation a été accepté</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /demandeParticipation
        ///     {
        ///        "NombrePlaces": Nombre de participant
        ///        "Courriel": Courriel de la personne applicante
        ///        "Nom": Nom de la personne applicante
        ///        "Prenom" Prenom de la personne applicante
        ///        "EvenementID": ID de l'evenement de la demande de participation
        ///     }
        ///
        /// </remarks>
        /// <response code="202">participation accepté avec succès</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="404">participation introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<ParticipationController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status202Accepted)]
        [ProducesResponseType(typeof(Participation), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Participation> Post([FromBody] DemandeParticipation demandeParticipation)
        {
            Participation? participation = _participationBL.Ajouter(demandeParticipation);
            return CreatedAtAction(nameof(GetById), new { id = participation.Id }, null);
        }
        /// <summary>
        /// Supprime une participation
        /// </summary>
        /// <param name="id">id de la participation à supprimer</param>
        /// <response code="204">participation supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">participation introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
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
        /// <summary>
        /// verification du status dune participation
        /// </summary>
        /// <param name="id">id de la participation à verifier</param>
        /// <response code="303">participation accepté, redirection à la participation</response>
        /// <response code="200">participation en attente d'un service externe</response>
        /// <response code="500">service indisponible pour le moment</response>
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
