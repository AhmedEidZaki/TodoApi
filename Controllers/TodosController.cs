using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTO;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly TodoApiContext _context;

        public TodosController(TodoApiContext context)
        {
            _context = context;
        }

        // GET: api/Todos
        [HttpGet]
        public async Task<IActionResult> GetAllTodos()
        {
            var todos = await _context.Todos.ToListAsync();
            return Ok(todos);
        }

        // GET: api/Todos/id
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
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
            var todos = await _context.Todos
                .Where(t => t.Title.Contains(title))
                .ToListAsync();

            return Ok(todos);
        }

        //POST: api/Todos
        [HttpPost]
        public async Task<IActionResult> CreateTodo(CreateTodoDto Todo)
        {
            var newTodo = new Model.Todo
            {
                Title = Todo.Title,
                Description = Todo.Description,
                IsCompleted = Todo.IsCompleted,
                CreatedAt = DateTime.UtcNow
            };
             await _context.Todos.AddAsync(newTodo);
             await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodo), new { id = newTodo.Id }, newTodo);
        }

        //PUT: api/Todos/id
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateTodo(int id, [FromBody] UpdateTodoDto todoupdate)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.Title = todoupdate.Title;
            todo.Description = todoupdate.Description;
            todo.IsCompleted = todoupdate.IsCompleted;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/Todos/id
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteTodo(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
