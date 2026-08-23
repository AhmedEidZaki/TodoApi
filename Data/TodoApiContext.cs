using Microsoft.EntityFrameworkCore;
using TodoApi.Model;

namespace TodoApi.Data
{
    public class TodoApiContext : DbContext
    {
        public DbSet<Todo> Todos { get; set; }

        public TodoApiContext(DbContextOptions<TodoApiContext> options)
            : base(options)
        {
        }

    }
    
}
