namespace TodoBackend.Dtos;

public record class UpdateTodoDto(
    string Name, 
    DateOnly DueDate, 
    bool IsCompleted
);
