using CsvReader.Infrastructure.Common;

namespace CsvReader.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
    }
}
