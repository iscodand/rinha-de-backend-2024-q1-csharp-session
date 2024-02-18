using Infrastructure.Data.Utils;
using Microsoft.EntityFrameworkCore;
using RinhaCrebito.Data;
using RinhaCrebito.DTOs.Responses;
using RinhaCrebito.Entities;
using RinhaCrebito.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Waiting for database
WaitForDatabase.Wait(builder.Configuration);

var app = builder.Build();

try
{
    app.ApplyMigrations();
    await app.SeedDatabaseAsync(CancellationToken.None);
}
catch (Exception ex)
{
    Console.WriteLine($"Error on migrate/seed database: {ex}");
}

app.MapGet("/clientes/{id}/extrato", async (int id, ApplicationDbContext context) =>
{
    if (await context.Customers.AnyAsync(x => x.Id == id) == false)
        return Results.NotFound();

    Customer customer = await context.Customers
                                                .AsNoTracking()
                                                .Include(x => x.Transactions)
                                                .Include(x => x.Balance)
                                                .Where(x => x.Id == id)
                                                .FirstOrDefaultAsync()
                                                .ConfigureAwait(false);

    ExtractResponse response = new()
    {
        Balance = CustomerResponse.Map(customer),
        Transactions = TransactionResponse.Map(customer.Transactions)
    };

    return Results.Ok(response);
});

app.Run();
