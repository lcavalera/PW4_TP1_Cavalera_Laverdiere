using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Microsoft.AspNetCore.Authorization;
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
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<VillesController>
        [HttpGet]
        [ProducesResponseType(typeof(List<VilleDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<VilleDTO>>> Get()
        {
            return Ok(await _villesBL.ObtenirTout());
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
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VilleDTO>> GetById(int id)
        {
            VilleDTO ville = await _villesBL.ObtenirSelonId(id);
            return ville == null ? NotFound() : Ok(ville);
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
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<VillesController>
        [HttpPost]
        [Authorize(Policy = "RequireAdmin")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] VilleDTO ville)
        {
            await _villesBL.Ajouter(ville);
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
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<VillesController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] VilleDTO ville)
        {
            await _villesBL.Modifier(id, ville);
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
        [Authorize(Policy = "RequireAdmin")]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _villesBL.Supprimer(id);
            return NoContent();
        }
    }
}
