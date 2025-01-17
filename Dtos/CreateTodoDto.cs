using System.ComponentModel.DataAnnotations;

namespace TodoBackend.Dtos;

public record class CreateTodoDto(
    [Required][StringLength(50)] 
    string Name, 
    
    [Range(typeof(DateOnly), "2025-01-01", "2050-12-31",
        ErrorMessage = "Value for {0} must be between {1} and {2}")]
    DateOnly DueDate
);
