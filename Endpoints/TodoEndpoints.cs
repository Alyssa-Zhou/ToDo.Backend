using System;
using Microsoft.AspNetCore.Http.HttpResults;
using TodoBackend.Dtos;
using TodoBackend.Interface;

namespace TodoBackend.Endpoints;

public static class TodoEndpoints
{
    const string GetTodoIdEndpoint = "GetId";
    public static RouteGroupBuilder MapTodoEndPoints(this WebApplication app)
    {
        var group = app.MapGroup("todos");

        // GET /todos
        group.MapGet("/", (ITaskService service) => service.GetTodosAsync());

        // GET /todos/done
        group.MapGet("/done", (ITaskService service) => service.GetCompletedTodosAsync());

        // GET /todos/undone
        group.MapGet("/undone", (ITaskService service) => service.GetUncompletedTodosAsync());

        // GET /todos/id
        group.MapGet("/{id}", GetTodoAsync)
            .WithName(GetTodoIdEndpoint);

        // POST /todos
        group.MapPost("/", async (CreateTodoDto task, ITaskService service) => 
        {
            var createdTask = await service.AddTodoAsync(task);

            return Results.CreatedAtRoute(
                GetTodoIdEndpoint, 
                new{ id = createdTask.Id }, // 找到game的Id
                createdTask);
        })
        .WithParameterValidation();

        // PUT /todos/id
        group.MapPut("/{id}", PutTodoAsync);

        // DELETE /todos/id
        group.MapDelete("/{id}", async (int id, ITaskService service) =>
        {
            await service.DeleteTodoByIdAsync(id);
            return TypedResults.NoContent();
        });

        return group;
    }

    private static async Task<IResult> PutTodoAsync(int id, UpdateTodoDto updatedTodo, ITaskService service)
    {
        if (await service.GetTodoByIdAsync(id) is null)
        {
            return TypedResults.NotFound();
        }
        await service.UpdateTodoAsync(id, updatedTodo);
        return TypedResults.NoContent();
    }

    private static async Task<IResult> GetTodoAsync(int id, ITaskService service)
    {
        var targetTodo = await service.GetTodoByIdAsync(id);
        if (targetTodo is null)
        {
            return TypedResults.NotFound();
        } 
        else{
            return TypedResults.Ok(targetTodo);
        }
    }
}
