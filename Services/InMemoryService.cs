using System;
using TodoBackend.Dtos;
using TodoBackend.Interface;

namespace TodoBackend.Services;

public class InMemoryService : ITaskService
{
    private readonly List<TodoDto> _todos = new()
    {
        new TodoDto(1, "Wash Dishes", DateOnly.Parse("2025-1-11"), false, false),
        new TodoDto(2, "Laundry", DateOnly.Parse("2025-1-11"), false, false),
        new TodoDto(3, "Buy Milk", DateOnly.Parse("2025-1-11"), true, false)
    };

    public async Task<TodoDto> AddTodoAsync(CreateTodoDto task)
    {
        TodoDto newTask = new(
            _todos.Count+1,
            task.Name,
            task.DueDate,
            false,
            false
        );
        _todos.Add(newTask);
        return await Task.FromResult(newTask);
    }

    public Task DeleteTodoByIdAsync(int id)
    {
        _todos.RemoveAll(task => id == task.Id);
        return Task.CompletedTask;
    }

    public Task<TodoDto?> GetTodoByIdAsync(int id)
    {
        return Task.FromResult(_todos.SingleOrDefault(task => id == task.Id));
    }
    public Task<List<TodoDto>> GetTodosAsync()
    {
        return Task.FromResult(_todos);
    }

    Task<List<TodoDto>> ITaskService.GetCompletedTodosAsync()
    {
        var done = _todos.Where(todo => todo.IsCompleted == true).ToList();
        return Task.FromResult(done);
    }

    Task<List<TodoDto>> ITaskService.GetUncompletedTodosAsync()
    {
        var done = _todos.Where(todo => todo.IsCompleted == false).ToList();
        return Task.FromResult(done);    
    }

    Task ITaskService.UpdateTodoAsync(int id, UpdateTodoDto updatedTodo)
    {
        var todoIndex = _todos.FindIndex(todo => todo.Id == id);
        _todos[todoIndex] = new(id, updatedTodo.Name, updatedTodo.DueDate, updatedTodo.IsCompleted, updatedTodo.IsPinned);
        return Task.CompletedTask;
    }
}
