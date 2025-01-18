using System;
using Microsoft.EntityFrameworkCore;
using TodoBackend.Data;
using TodoBackend.Dtos;
using TodoBackend.Entities;
using TodoBackend.Interface;
using TodoBackend.Mapping;

namespace TodoBackend.Services;

public class DatabaseService : ITaskService
{
    private readonly TodoContext todoContext;

    public DatabaseService(TodoContext context)
    {
        todoContext = context ?? throw new ArgumentNullException(nameof(context));
    }

    async Task<TodoDto> ITaskService.AddTodoAsync(CreateTodoDto task)
    {
        Todo todo = task.ToEntity();
        await todoContext.Todos.AddAsync(todo);
        await todoContext.SaveChangesAsync();
        return todo.ToDto();
    }

    async Task ITaskService.DeleteTodoByIdAsync(int id)
    {
        await todoContext.Todos.Where(todo => todo.Id == id).ExecuteDeleteAsync();
    }

    async Task<TodoDto?> ITaskService.GetTodoByIdAsync(int id)
    {
        Todo? todo = await todoContext.Todos.FindAsync(id);
        return todo?.ToDto();
    }

    async Task<List<TodoDto>> ITaskService.GetTodosAsync()
    {
        return await todoContext.Todos.Select(todo => todo.ToDto()).AsNoTracking().ToListAsync();
    }

    async Task<List<TodoDto>> ITaskService.GetCompletedTodosAsync()
    {
        return await todoContext.Todos.Where(todo => todo.IsCompleted == true)
        .Select(todo => todo.ToDto()).AsNoTracking().ToListAsync();
    }

    async Task<List<TodoDto>> ITaskService.GetUncompletedTodosAsync()
    {
        return await todoContext.Todos.Where(todo => todo.IsCompleted == false)
        .Select(todo => todo.ToDto()).AsNoTracking().ToListAsync();
    }

    async Task ITaskService.UpdateTodoAsync(int id, UpdateTodoDto updatedTodo)
    {
        Todo? existingTodo = await todoContext.Todos.FindAsync(id);
        
        if(existingTodo is not null)
        {
            todoContext.Entry(existingTodo).CurrentValues.SetValues(updatedTodo.ToEntity(id));
            await todoContext.SaveChangesAsync();
        }
    }
}
