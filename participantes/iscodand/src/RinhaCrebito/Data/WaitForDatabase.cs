using Microsoft.Extensions.Configuration;
using System.Net.Sockets;

namespace Infrastructure.Data.Utils
{
    public class WaitForDatabase
    {
        public static void Wait(IConfiguration configuration)
        {
            // Isco 27/11/2023
            // Recuperando Server e Porta do Banco de Dados utilizado
            string databaseAddress = configuration["DatabaseData:Server"];
            _ = int.TryParse(configuration["DatabaseData:Port"],
                 out int databasePort);
            int timeoutInMinutes = 1;

            DateTime startTime = DateTime.Now;

            while (DateTime.Now < startTime.AddMinutes(timeoutInMinutes))
            {
                try
                {
                    Console.WriteLine("...Testing...");

                    using TcpClient client = new();
                    client.Connect(databaseAddress, databasePort);

                    if (client.Connected)
                    {
                        Console.WriteLine("Database is ready!");
                        Thread.Sleep(5000);
                        return;
                    }
                }
                catch
                {
                    Console.WriteLine("Database is not ready!\n...Waiting 3 seconds before try again...");
                    Thread.Sleep(3000);
                }
            }

            throw new TimeoutException("Timeout exceeded. Database Problems.");
        }
    }
}