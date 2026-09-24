using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTO;
using TodoApi.Services;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly ITodoService _todoService;


        public TodosController(ITodoService todoService)
        {
            _todoService = todoService;
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _todoService.GetTodosAsync();
            return Ok(todos);   
        }

        // GET: api/Todos/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _todoService.GetTodoAsync(id);

            if (todo == null)
            {
                return NotFound();
            }

            return Ok(todo);
        }

        //GET: api/Todos/search?title=someTitle
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string title)
        {
            var todos = await _todoService.SearchAsync(title);
            return Ok(todos);
        }

        //POST: api/Todos
        [HttpPost]
        public async Task<IActionResult> CreateTodo(CreateTodoDto Todo)
        {
            var newTodo = await _todoService.CreateTodoAsync(Todo);

            return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
        }

        //PUT: api/Todos/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto todoupdate)
        {
            var todo = await _todoService.UpdateTodoAsync(id, todoupdate);

            if (todo == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        //PATCH: api/Todos/id/complete
        [HttpPatch("{id:int}/complete")]
        public async Task<IActionResult> CompleteTodo(int id)
        {
            var success = await _todoService.CompleteTodoAsync(id);

            if (!success)
            {
                return NotFound();
            }

            return NoContent();
        }

        //DELETE: api/Todos/id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _todoService.DeleteTodoAsync(id);

            if(!todo)
            {
                return NotFound();
            }
            return NoContent();
        }
    }
}
