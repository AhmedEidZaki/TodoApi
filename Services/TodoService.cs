using Microsoft.EntityFrameworkCore;
using TodoApi.Data;
using TodoApi.DTO;
using TodoApi.Model;

namespace TodoApi.Services
{
    public class TodoService : ITodoService
    {
        private readonly TodoApiContext _context;

        public TodoService(TodoApiContext context)
        {
            _context = context;
        }


        public async Task<List<Todo>> GetTodosAsync()
        {
            return await _context.Todos.ToListAsync();
        }
        public async Task<Todo> GetTodoAsync(int id)
        {
            return await _context.Todos.FindAsync(id);
        }
        public async Task<List<Todo>> SearchAsync(string title)
        {
           return await _context.Todos
                .Where(t => t.Title.Contains(title))
                .ToListAsync();
        }
        public async Task<Todo> CreateTodoAsync(CreateTodoDto todoDto)
        {
            var newTodo = new Todo
            {
                Title = todoDto.Title,
                Description = todoDto.Description,
                IsCompleted = todoDto.IsCompleted,
                CreatedAt = DateTime.UtcNow
            };
            await _context.Todos.AddAsync(newTodo);
            await _context.SaveChangesAsync();
            return newTodo;
        }
        public async Task<Todo> UpdateTodoAsync(int id,UpdateTodoDto todoDto)
        {
            var todo =  await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return null;
            }

            todo.Title = todoDto.Title;
            todo.Description = todoDto.Description;
            todo.IsCompleted = todoDto.IsCompleted;

            await _context.SaveChangesAsync();
            return todo;
        }
        public async Task<bool> CompleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return false;
            }

            todo.IsCompleted = true;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteTodoAsync(int id)
        {
            var todo = await _context.Todos.FindAsync(id);
            if (todo == null)
            {
                return false;
            }

            _context.Todos.Remove(todo);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
