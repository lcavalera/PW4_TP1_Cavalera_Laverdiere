using Events.Api.BusinessLogic;
using Events.Api.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]

    public class UsagersController : ControllerBase
    {
        private readonly IUsagersBL _usagersBL;

        public UsagersController(IUsagersBL usagerBL)
        {
            _usagersBL = usagerBL;
        }

        /// <summary>
        /// Retourne une liste des usagers 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/usagers
        ///
        /// </remarks>
        /// <response code="200">usagers trouvés et retournés</response>
        /// <response code="404">usagers introuvables</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<UsagersController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Usager>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<IEnumerable<Usager>> Get()
        {
            return Ok(_usagersBL.ObtenirTout());
        }

        /// <summary>
        /// Retourne un usager spécifique à partir de son id
        /// </summary>
        /// <param name="id">id du usager à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Usager/1
        ///
        /// </remarks>
        /// <response code="200">usager trouvé et retourné</response>
        /// <response code="404">usager introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<UsagersController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Usager> GetById(int id)
        {
            var usager = _usagersBL.ObtenirSelonId(id);
            return usager == null ? NotFound(new { Erreur = $"Usager introuvable (id = {id}" }) : Ok(usager);
        }

        /// <summary>
        /// Ajoute un usager à la base de donnée
        /// </summary>
        /// <param name="usager">usager à ajouter</param>
        /// <returns>Un neuveau usager a été créé</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Usager
        ///     {
        ///        "id": 1,
        ///        "nom": "nom de l'usager",
        ///        "prenom": "prenom de l'usager",
        ///        "telephone": "telephone de l'usager",
        ///        "dateNaissance": "date de naissance de l'usager",
        ///        "courriel": "courriel de l'usager"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">usager ajouté avec succès</response>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">Model Invalide, mauvaise requête</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<UsagersController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Usager usager)
        {
            usager = _usagersBL.Ajouter(usager);
            return CreatedAtAction(nameof(GetById), new { id = usager.Id }, null);
        }

        /// <summary>
        /// Modification d'un usager
        /// </summary>
        /// <param name="id">id de l'usager à modifier</param>
        /// <param name="usager"></param>
        /// <returns>L'usager a été modifié</returns>
        /// <response code="200">traitement executé avec succès, contenu retourné</response>
        /// <response code="204">usager modifié avec succès, aucune contenu retourné</response>
        /// <response code="404">usager introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<UsagersController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Usager usager)
        {
            _usagersBL.Modifier(id, usager);
            return NoContent();
        }

        /// <summary>
        /// Supprime un usager
        /// </summary>
        /// <param name="id">id de l'usager à supprimer</param>
        /// <response code="204">usager supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">usager introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<UsagersController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Usager), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _usagersBL.Supprimer(id);
            return NoContent();
        }
    }
}
