namespace CsvReader.Application.Common.Exceptions
{
    public class CsvToDbTransferException : Exception
    {
        public CsvToDbTransferException() { }

        public CsvToDbTransferException(string message) : base(message) { }

        public CsvToDbTransferException(string message, Exception innerException) : base(message, innerException) { }
    }
}
