namespace CsvReader.Application.Common.Intrefaces
{
    public interface ICsvFileReader : IDisposable
    {
        public int NumberFirstRowSkips { get; }
        public bool ReadNextRecord();
        public void SkipRows(int countRow);
        public string[] GetCurrentValuesRecord();

        public int MaxFieldCount { get; }
    }
}
