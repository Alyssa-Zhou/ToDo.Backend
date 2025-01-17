using System;
using Microsoft.EntityFrameworkCore;

namespace TodoBackend.Data;

public static class DataExtensions
{
    public static void MigrateDb (this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<TodoContext>();
        service.Database.Migrate();
    }
}
