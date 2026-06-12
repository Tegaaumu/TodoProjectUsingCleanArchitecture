using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TodoProjectUsingCleanArchitecture.Application.Models;
using TodoProjectUsingCleanArchitecture.Application.Repositories;
using TodoProjectUsingCleanArchitecture.Application.Services;
using TodoProjectUsingCleanArchitecture.Contract.Request;
using TodoProjectUsingCleanArchitecture.Presentation.Mapping;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TodoProjectUsingCleanArchitecture.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoListController : ControllerBase
    {
        private readonly ITodoListServices _todoListServices;
        public TodoListController(ITodoListServices todoListServices)
        {
            _todoListServices= todoListServices;
        }
        // GET: api/<TodoListController>
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            //var tasks = await _todoListRepositories.GetAllAsync();
            var tasks = await _todoListServices.GetAllByDtoAsync();
            return Ok(tasks);
        }

        // GET api/<TodoListController>/5
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get(Guid id)
        {
            var task = await _todoListServices.GetByIdAsync(id);
            if (task == null)
                return NotFound("This id is incorrect");
            return Ok(task);
        }

        // POST api/<TodoListController>
        [HttpPost]
    // [ProducesResponseType(typeof(MovieReponse), StatusCodes.Status201Created)]
        public async Task<IActionResult> Post([FromBody] TaskItemRequest request)
        {

            var movie = request.MapToList();

            await _todoListServices.CreateAync(movie);

            return CreatedAtAction(nameof(Post), new { id = movie.Id }, movie);
        }

        // PUT api/<TodoListController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Guid id, TaskItemRequest request)
        {
            var todoList = request.MapToList(id);
            await _todoListServices.UpdateAync(todoList);
            return CreatedAtAction(nameof(Put), new { id = todoList.Id }, todoList);
        }

        // DELETE api/<TodoListController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var task = await _todoListServices.GetByIdAsync(id);
            if (task is null)
                return NotFound();
            return Ok();
        }
    }
}
