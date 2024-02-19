using Infrastructure.Data.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RinhaCrebito.Data;
using RinhaCrebito.DTOs.Requests;
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
    Customer customer = await context.Customers.AsNoTracking()
                                            .Include(x => x.Transactions)
                                            .Include(x => x.Balance)
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync()
                                            .ConfigureAwait(false);

    if (customer is null)
        return Results.NotFound();

    ExtractResponse response = new()
    {
        Balance = CustomerResponse.Map(customer),
        Transactions = TransactionResponse.Map(customer.Transactions)
    };

    return Results.Ok(response);
});


app.MapPost("clientes/{id}/transacoes", async (int id, [FromBody] MakeTransactionRequest request, ApplicationDbContext context) =>
{
    Customer customer = await context.Customers.AsNoTracking()
                                            .Include(x => x.Balance)
                                            .Where(x => x.Id == id)
                                            .FirstOrDefaultAsync()
                                            .ConfigureAwait(false);

    if (customer is null)
        return Results.NotFound();

    int newBalance;

    if (request.Type == "d")
    {
        if ((customer.Balance.Total - request.Value) < -customer.Limit)
        {
            return Results.StatusCode(422);
        }

        newBalance = customer.Balance.Total - request.Value;
    }
    else
    {
        newBalance = customer.Balance.Total + request.Value;
    }

    // valida se:
    // 1. descricao é nula
    // 2. descricao é do tipo string
    // 3. descricao tem mais de 0 caracteres
    // 4. descricao tem menos de 10 caracteres

    if (request.Description is null ||
        request.Description.GetType() != typeof(string) ||
        request.Description.Length == 0 ||
        request.Description.Length > 10)
    {
        return Results.StatusCode(422);
    }

    Transaction transaction = Transaction.Create(customer.Id, request.Value, request.Type, request.Description);
    customer.Balance.Total = newBalance;

    await context.Transactions.AddAsync(transaction);
    context.Balances.Update(customer.Balance);

    await context.SaveChangesAsync();

    MakeTransactionResponse response = new()
    {
        Balance = newBalance,
        Limit = customer.Limit
    };

    return Results.Ok(response);
});

app.Run();
