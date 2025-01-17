using System;
using Microsoft.EntityFrameworkCore;
using TodoBackend.Entities;

namespace TodoBackend.Data;

public class TodoContext(DbContextOptions<TodoContext> options) : DbContext(options)
{
    public DbSet<Todo> Todos => Set<Todo>();


}
