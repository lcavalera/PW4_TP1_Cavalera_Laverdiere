using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class EvenementsController : ControllerBase
    {
        private readonly IEvenementsBL _evenementsBL;

        public EvenementsController(IEvenementsBL evenementsBL)
        {
            _evenementsBL = evenementsBL;
        }

        /// <summary>
        /// Retourne une liste des evenements 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/evenements
        ///
        /// </remarks>
        /// <response code="200">evenements trouvés et retournés</response>
        /// <response code="404">evenements introuvables</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<EvenementsController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Evenement>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Evenement>> Get()
        {
            return Ok(_evenementsBL.ObtenirTout());
        }

        /// <summary>
        /// Retourne un'evenement spécifique à partir de son id
        /// </summary>
        /// <param name="id">id de l'evenement à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Evenement/1
        ///
        /// </remarks>
        /// <response code="200">evenement trouvé et retourné</response>
        /// <response code="404">evenement introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<EvenementsController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Evenement> GetById(int id)
        {
            var evenement = _evenementsBL.ObtenirSelonId(id);
            return evenement == null ? NotFound(new { Erreur = $"Evenement introuvable (id = {id}" }) : Ok(evenement);
        }

        /// <summary>
        /// Ajoute un'evenement à la base de donnée
        /// </summary>
        /// <param name="evenement">evenement à ajouter</param>
        /// <returns>Un nouveau evenement a été créé</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Evenement
        ///     {
        ///        "id": 1,
        ///        "dateDebut": "date de debut de l'evenement",
        ///        "dateDeFin": "date de fin de l'evenement"
        ///        "Titre": "titre de l'evenement",
        ///        "Description": "description de l'evenement",
        ///        "Categories": "liste de categories de l'evenement"
        ///        "Adresse": "adresse de l'evenement",
        ///        "NomOrganisateur": "nom de l'organisateur de l'evenement"
        ///        "Ville": "Ville de l'evenement",
        ///        "Prix": "Prix de l'evenement"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">evenement ajouté avec succès</response>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">Model Invalide, mauvaise requête</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<EvenementsController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Evenement evenement)
        {
            evenement = _evenementsBL.Ajouter(evenement);
            return CreatedAtAction(nameof(GetById), new { id = evenement.Id }, null);
        }

        /// <summary>
        /// Modification d'un'evenement
        /// </summary>
        /// <param name="id">id de l'evenement à modifier</param>
        /// <param name="ville"></param>
        /// <returns>La evenement a été modifié</returns>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">evenement modifié avec succès, aucune contenu retourné</response>
        /// <response code="404">evenement introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<EvenementsController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Evenement), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Evenement evenement)
        {
            _evenementsBL.Modifier(id, evenement);
            return NoContent();
        }

        /// <summary>
        /// Supprime une ville
        /// </summary>
        /// <param name="id">id de la ville à supprimer</param>
        /// <response code="204">ville supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<EvenementsController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _evenementsBL.Supprimer(id);
            return NoContent();
        }
    }
}
