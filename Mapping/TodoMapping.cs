using System;
using TodoBackend.Dtos;
using TodoBackend.Entities;

namespace TodoBackend.Mapping;

public static class TodoMapping
{
    public static Todo ToEntity(this TodoDto todoDto){
        return new(){Id = todoDto.Id,
                   Name = todoDto.Name,
                   DueDate = todoDto.DueDate,
                   IsCompleted = todoDto.IsCompleted};
    }

    public static Todo ToEntity(this CreateTodoDto todoDto){
        return new(){
                   Name = todoDto.Name,
                   DueDate = todoDto.DueDate,
                   IsCompleted = false};
    }

    public static Todo ToEntity(this UpdateTodoDto todoDto, int id){
        return new(){
                    Id = id,
                    Name = todoDto.Name,
                    DueDate = todoDto.DueDate,
                    IsCompleted = todoDto.IsCompleted};
    }

    public static TodoDto ToDto(this Todo todo){
        return new(todo.Id, todo.Name, todo.DueDate, todo.IsCompleted);
    }
}
