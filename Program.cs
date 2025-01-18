using System.Xml.Linq;
using DotNetEnv;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Rewrite;
using TodoBackend.Data;
using TodoBackend.Dtos;
using TodoBackend.Endpoints;
using TodoBackend.Interface;
using TodoBackend.Services;

var builder = WebApplication.CreateBuilder(args);

Env.Load();
builder.Configuration.AddEnvironmentVariables();

// To Use in memory service
// builder.Services.AddSingleton<ITaskService, InMemoryService>();

// // To Use database service
var connString = builder.Configuration.GetConnectionString("Todo");
builder.Services.AddSqlite<TodoContext>(connString);
// Comment if use in memory service
builder.Services.AddScoped<ITaskService, DatabaseService>();

var app = builder.Build();

//Middleware

// Use built-in middleware to redirect tasks/x to todos/x
app.UseRewriter(new RewriteOptions().AddRedirect("tasks/(.*)", "todos/$1"));

// Customize middleware
app.Use(async (context, next) =>
{
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Started.");
    await next(context);
    Console.WriteLine($"[{context.Request.Method} {context.Request.Path} {DateTime.UtcNow}] Finished.");
});

app.MapTodoEndPoints();

// To migrate database when set up
app.MigrateDb();

app.Run();

