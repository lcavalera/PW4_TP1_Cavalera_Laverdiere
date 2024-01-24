using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class VillesController : ControllerBase
    {
        private readonly IVillesBL _villesBL;

        public VillesController(IVillesBL villesBL)
        {
            _villesBL = villesBL;
        }

        /// <summary>
        /// Retourne une liste des villes 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/villes
        ///
        /// </remarks>
        /// <response code="200">villes trouvés et retournés</response>
        /// <response code="404">villes introuvables</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<VillesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Ville>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Ville>> Get()
        {
            return Ok(_villesBL.ObtenirTout());
        }

        /// <summary>
        /// Retourne une ville spécifique à partir de son id
        /// </summary>
        /// <param name="id">id de la ville à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Ville/1
        ///
        /// </remarks>
        /// <response code="200">ville trouvé et retourné</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<VillesController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Ville> GetById(int id)
        {
            var ville = _villesBL.ObtenirSelonId(id);
            return ville == null ? NotFound(new { Erreur = $"Ville introuvable (id = {id}" }) : Ok(ville);
        }

        /// <summary>
        /// Ajoute une ville à la base de donnée
        /// </summary>
        /// <param name="ville">ville à ajouter</param>
        /// <returns>Une nouvelle ville a été créé</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Ville
        ///     {
        ///        "id": 1,
        ///        "nom": "nom de la ville",
        ///        "region": "region de la ville"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">ville ajouté avec succès</response>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">Model Invalide, mauvaise requête</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<VillesController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Ville ville)
        {
            ville = _villesBL.Ajouter(ville);
            return CreatedAtAction(nameof(GetById), new { id = ville.Id }, null);
        }

        /// <summary>
        /// Modification d'une ville
        /// </summary>
        /// <param name="id">id de la ville à modifier</param>
        /// <param name="ville"></param>
        /// <returns>La ville a été modifié</returns>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">ville modifié avec succès, aucune contenu retourné</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<VillesController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Ville ville)
        {
            _villesBL.Modifier(id, ville);
            return NoContent();
        }

        /// <summary>
        /// Supprime une ville
        /// </summary>
        /// <param name="id">id de la ville à supprimer</param>
        /// <response code="204">ville supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<UsagersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _villesBL.Supprimer(id);
            return NoContent();
        }
    }
}
