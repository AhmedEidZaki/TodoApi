using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TodoApi.DTO
{
    public class CreateTodoDto
    {
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = string.Empty;
        [MaxLength(500)]
        public string Description { get; set; } = string.Empty;

        [DefaultValue(false)]
        public bool IsCompleted { get; set; } 
    }
}
