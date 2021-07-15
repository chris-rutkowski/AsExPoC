using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AsExPoC
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                var host = CreateHostBuilder(args).Build();
                var service = host.Services.GetRequiredService<DummyService>();
                // Tried: service.TestExceptionSync(); // Awesome! Exited with error code 8
                service.Run();

                // other services are running too - I cannot just wait for DummyService to throw the exception

                host.Run();

                return 0;
            }
            catch (ForcedExitException)
            {
                Console.WriteLine("Forcede exit exception");
                return 8;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Generc exception {ex.ToString()}");
                return 1;
            }
            finally
            {
                Console.WriteLine("finally");
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
