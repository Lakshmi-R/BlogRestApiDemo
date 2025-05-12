using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repository.Interface;
using Repository.Models;

namespace BlogRestApiDemo.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository _authorRepository;
        public AuthorController(IAuthorRepository authorRepository)
        {
            _authorRepository = authorRepository;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
           var res = await _authorRepository.GetAllAsync();
            return Ok(res);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Author author)
        {
            await _authorRepository.CreateAuthorAsync(author);
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var res = await _authorRepository.GetAuthorByIdAsync(id);
            return Ok(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Author author)
        {
            if (id != author.Id) { return BadRequest(); }
            var updated = await _authorRepository.UpdateAuthorAsync(author);
            return updated ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _authorRepository.DeleteAuthorAsync(id); 
            return deleted ? Ok() : NotFound();
        }
    }
}
