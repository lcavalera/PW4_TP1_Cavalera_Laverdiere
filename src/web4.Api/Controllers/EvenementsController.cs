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
        /// <param name="filtre">filtre pour titre ou description des evenements à retourner</param>
        /// <remarks>
        /// 
        ///     GET api/evenements
        ///
        /// </remarks>
        /// <response code="200">evenements trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<EvenementsController>
        [HttpGet]
        [ProducesResponseType(typeof(List<EvenementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvenementDTO>>> Get(string filtre, int pageIndex = 1, int pageCount = 10)
        {
            var evenements = await _evenementsBL.ObtenirTout();

            if(filtre != null)
            {
                evenements = evenements.Where(e => e.Titre.Contains(filtre) || e.Description.Contains(filtre)).ToList();
            }

            return Ok(evenements.OrderBy(e=> e.DateDebut).Skip((pageIndex - 1) * pageCount).Take(pageCount));
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
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<EvenementDTO>> GetById(int id)
        {
            EvenementDTO evenement = await _evenementsBL.ObtenirSelonId(id);
            return evenement == null ? NotFound() : Ok(evenement);
        }

        /// <summary>
        /// Retourne une liste des evenements à partir de l'id de la ville spécifiée
        /// </summary>
        /// <param name="villeId">id de la ville de les evenements à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Evenement/1/Ville
        ///
        /// </remarks>
        /// <response code="200">evenements trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        [HttpGet("{villeId}/ville")]
        [ProducesResponseType(typeof(IEnumerable<EvenementDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<EvenementDTO>>> GetByIdVille(int villeId)
        {
            return Ok(await _evenementsBL.ObtenirSelonIdVille(villeId));
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
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="404">ville introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<EvenementsController>
        [HttpPost]
        [Authorize(Policy = "RequireManager")]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(VilleDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] EvenementDTO evenement)
        {
            await _evenementsBL.Ajouter(evenement);
            return CreatedAtAction(nameof(GetById), new { id = evenement.Id }, null);
        }

        /// <summary>
        /// Modification d'un'evenement
        /// </summary>
        /// <param name="id">id de l'evenement à modifier</param>
        /// <param name="evenement"></param>
        /// <returns>La evenement a été modifié</returns>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">evenement modifié avec succès, aucune contenu retourné</response>
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="404">evenement introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<EvenementsController>/5
        [HttpPut("{id}")]
        [Authorize(Policy = "RequireManager")]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] EvenementDTO evenement)
        {
            await _evenementsBL.Modifier(id, evenement);
            return NoContent();
        }

        /// <summary>
        /// Supprime un evenement
        /// </summary>
        /// <param name="id">id de l'evenement à supprimer</param>
        /// <response code="204">evenement supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">evenement introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<EvenementsController>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "RequireManager")]
        [ProducesResponseType(typeof(EvenementDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _evenementsBL.Supprimer(id);
            return NoContent();
        }
    }
}
