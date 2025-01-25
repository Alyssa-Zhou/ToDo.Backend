namespace TodoBackend.Dtos;

public record class TodoDto(
    int Id, 
    string Name, 
    DateOnly DueDate, 
    bool IsCompleted,
    bool IsPinned
);

