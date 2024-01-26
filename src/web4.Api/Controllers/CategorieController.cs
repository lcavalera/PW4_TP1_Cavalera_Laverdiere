using Events.Api.BusinessLogic;
using Events.Api.Data;
using Events.Api.Entites;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Events.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CategorieController(ICategorieBL categorieBL) : ControllerBase
    {
        private readonly ICategorieBL _categorieBL = categorieBL;
        // GET: api/<CategorieController>
        [HttpGet]
        [ProducesResponseType(typeof(List<Categorie>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<List<Categorie>> Get()
        {
            return Ok(_categorieBL.ObtenirTout());
        }

        // GET api/<CategorieController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<Categorie> GetById(int id)
        {
            Categorie? categorie = _categorieBL.ObtenirSelonId(id);
            return categorie != null ? Ok(categorie) : NotFound();
        }

        // POST api/<CategorieController>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Ville), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] Categorie categorie)
        {
            categorie = _categorieBL.Ajouter(categorie);
            return CreatedAtAction(nameof(GetById), new { id = categorie.Id }, null);
        }

        // PUT api/<CategorieController>/5
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Put(int id, [FromBody] Categorie categorie)
        {
            _categorieBL.Modifier(id, categorie);
            return NoContent();
        }

        // DELETE api/<CategorieController>/5
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Categorie), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(int id)
        {
            _categorieBL.Supprimer(id);
            return NoContent();
        }
    }
}
