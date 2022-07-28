namespace CsvReader.Console.Interfaces
{
    public interface ISettings
    {
        public bool HasHeaders { get; set; }
        public char Delimeter { get; set; }
        public int NumberOfLinesToSkipInTheStartFile { get; set; }
        public int BufferSize { get; set; }
        public string PathCsv { get; set; }
        public string EncodingCsv { get; set; }
        public string ConnectionDbString { get; set; }
    }
}
