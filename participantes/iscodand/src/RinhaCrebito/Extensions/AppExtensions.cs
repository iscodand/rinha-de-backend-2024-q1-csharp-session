using Microsoft.EntityFrameworkCore;
using RinhaCrebito.Data;
using RinhaCrebito.Seeds;

namespace RinhaCrebito.Extensions
{
    public static class AppExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using IServiceScope serviceScope = app.ApplicationServices.CreateScope();
            serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
        }

        public static async Task SeedDatabaseAsync(this IApplicationBuilder app, CancellationToken cancellationToken)
        {
            using IServiceScope scope = app.ApplicationServices.CreateScope();
            {
                ApplicationDbContext context = (ApplicationDbContext)scope
                    .ServiceProvider
                    .GetService(typeof(ApplicationDbContext));

                await DefaultSeed.SeedAsync(context, cancellationToken);
            }
        }
    }
}