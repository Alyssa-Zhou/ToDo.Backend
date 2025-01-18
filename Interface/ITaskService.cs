using System;
using TodoBackend.Dtos;

namespace TodoBackend.Interface;

public interface ITaskService
{
    Task<TodoDto?> GetTodoByIdAsync(int id); // Return maybe Todo or null.
    Task<List<TodoDto>> GetTodosAsync();
    Task<List<TodoDto>> GetUncompletedTodosAsync();
    Task<List<TodoDto>> GetCompletedTodosAsync();

    Task DeleteTodoByIdAsync(int id);
    Task<TodoDto> AddTodoAsync(CreateTodoDto task);
    Task UpdateTodoAsync(int id, UpdateTodoDto updatedTodo);
}
