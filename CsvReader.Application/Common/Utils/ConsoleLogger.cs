using CsvReader.Application.Common.Intrefaces;

namespace CsvReader.Application.Common.Utils
{
    public class ConsoleLogger : ILogger
    {
        public void Log(string text)
        {
            Console.WriteLine($"[{DateTime.Now}] : " + text);
        }
    }
}
