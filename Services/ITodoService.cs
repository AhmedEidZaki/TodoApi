using TodoApi.DTO;
using TodoApi.Model;

namespace TodoApi.Services
{
    public interface ITodoService
    {
        Task<List<Todo>> GetTodosAsync();
        Task<Todo> GetTodoAsync(int id);
        Task<List<Todo>> SearchAsync(string title);
        Task<Todo> CreateTodoAsync(CreateTodoDto todoDto);
        Task<Todo> UpdateTodoAsync(int id,UpdateTodoDto todoDto);
        Task<bool> CompleteTodoAsync(int id);
        Task<bool> DeleteTodoAsync(int id);
    }
}
