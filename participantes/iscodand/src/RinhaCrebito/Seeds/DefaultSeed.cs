using Microsoft.EntityFrameworkCore;
using RinhaCrebito.Data;
using RinhaCrebito.Entities;

namespace RinhaCrebito.Seeds
{
    public static class DefaultSeed
    {
        public static async Task SeedAsync(ApplicationDbContext context, CancellationToken cancellationToken)
        {
            if (await context.Customers.AnyAsync(x => x.Id == 1, cancellationToken) == false)
            {
                Customer customer1 = Customer.Create("o barato sai caro", 1000 * 100);
                Balance balance1 = Balance.Create(customer1.Id);
                customer1.Balance = balance1;

                Customer customer2 = Customer.Create("zan corp ltda", 800 * 100);
                Balance balance2 = Balance.Create(customer2.Id);
                customer2.Balance = balance2;

                Customer customer3 = Customer.Create("les cruders", 10000 * 100);
                Balance balance3 = Balance.Create(customer3.Id);
                customer3.Balance = balance3;

                Customer customer4 = Customer.Create("padaria joia de cocaina", 100000 * 100);
                Balance balance4 = Balance.Create(customer4.Id);
                customer4.Balance = balance4;

                Customer customer5 = Customer.Create("kid mais", 5000 * 100);
                Balance balance5 = Balance.Create(customer5.Id);
                customer5.Balance = balance5;

                await context.Customers.AddRangeAsync([customer1, customer2,
                                                  customer3, customer4,
                                                  customer5]);

                await context.SaveChangesAsync(cancellationToken);
            }
        }
    }
}