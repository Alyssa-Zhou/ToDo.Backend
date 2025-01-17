using System;

namespace TodoBackend.Entities;

public class Todo
{
    public int Id { get; set; } 
    public required string Name { get; set; } 
    public DateOnly DueDate { get; set; } 
    public bool IsCompleted { get; set; } 
}
