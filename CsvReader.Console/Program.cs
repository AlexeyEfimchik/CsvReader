using Microsoft.Extensions.Configuration;

namespace CsvReader.Console
{
    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

                Startup startup = new Startup(configuration);
                startup.Start();
            }
            catch (Exception ex)
            {
                System.Console.WriteLine(ex.Message);
            }
        }
    }
}