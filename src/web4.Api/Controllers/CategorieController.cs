using Events.Api.BusinessLogic.Interfaces;
using Events.Api.Data;
using Events.Api.Entites;
using Events.Api.Entites.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] 

    public class CategorieController(ICategorieBL categorieBL) : ControllerBase
    {
        private readonly ICategorieBL _categorieBL = categorieBL;
        /// <summary>
        /// Retourne une liste des Categories 
        /// </summary>
        /// <remarks>
        /// 
        ///     GET api/Categorie
        ///
        /// </remarks>
        /// <response code="200">Categories trouvés et retournés</response>
        /// <response code="500">service indisponible pour le moment</response>
        /// <returns></returns>
        // GET: api/<CategorieController>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategorieDTO>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<CategorieDTO>>> Get()
        {
            return Ok(await _categorieBL.ObtenirTout());
        }


        /// <summary>
        /// Retourne une Categorie spécifique à partir de son id
        /// </summary>
        /// <param name="id">id de la Categorie à retourner</param>
        /// <remarks>
        /// 
        ///     GET /Categorie/1
        ///
        /// </remarks>
        /// <response code="200">Categorie trouvé et retourné</response>
        /// <response code="404">Categorie introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // GET api/<CategorieController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CategorieDTO>> GetById(int id)
        {
            CategorieDTO? categorie = await _categorieBL.ObtenirSelonId(id);
            return categorie != null ? Ok(categorie) : NotFound();
        }

        /// <summary>
        /// Ajoute une Categorie à la base de donnée
        /// </summary>
        /// <param name="categorie">Categorie à ajouter</param>
        /// <returns>Une nouvelle Categorie a été créé</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /Categorie
        ///     {
        ///        "id": 1,
        ///        "nom": "nom de la Categorie"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Categorie ajouté avec succès</response>
        /// <response code="204">traitement executé avec succès, aucune contenu retourné</response>
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="500">service indisponible pour le moment</response>
        // POST api/<CategorieController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(CategorieDTO), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CategorieDTO categorie)
        {
            await _categorieBL.Ajouter(categorie);
            return CreatedAtAction(nameof(GetById), new { id = categorie.Id }, null);
        }

        /// <summary>
        /// Modification d'une Categorie
        /// </summary>
        /// <param name="id">id de la Categorie à modifier</param>
        /// <param name="categorie"></param>
        /// <returns>La Categorie a été modifié</returns>
        /// <response code="204">Categorie modifié avec succès, aucune contenu retourné</response>
        /// <response code="400">model Invalide, mauvaise requête</response>
        /// <response code="404">Categorie introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // PUT api/<CategorieController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, [FromBody] CategorieDTO categorie)
        {
            await _categorieBL.Modifier(id, categorie);
            return NoContent();
        }

        /// <summary>
        /// Supprime une Categorie
        /// </summary>
        /// <param name="id">id de la Categorie à supprimer</param>
        /// <response code="204">Categorie supprimé avec succès, aucune contenu retourné</response>
        /// <response code="404">Categorie introuvable pour l'id spécifié</response>
        /// <response code="500">service indisponible pour le moment</response>
        // DELETE api/<CategorieController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await _categorieBL.Supprimer(id);
            return NoContent();
        }
    }

    internal class AutorizeAttribute : Attribute
    {
    }
}
