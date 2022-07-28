using CsvReader.Application.Common.Intrefaces;
using LumenWorks.Framework.IO.Csv;


namespace CsvReader.Application.Common.Utils
{
    public class CsvFileReader : ICsvFileReader
    {
        private LumenWorks.Framework.IO.Csv.CsvReader csvReader;
        private TextReader textReader;
        private int maxFieldCount;
        private bool disposed;
        private ILogger logger;
        public int NumberFirstRowSkips { get; }

        public int MaxFieldCount => maxFieldCount;

        public CsvFileReader(TextReader textReader, bool hasHeaders, char delimeter, ILogger logger, int numberFirstRowSkips = 0)
        {
            this.textReader = textReader ?? throw new ArgumentNullException(nameof(textReader));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (numberFirstRowSkips < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(numberFirstRowSkips));
            }

            this.NumberFirstRowSkips = numberFirstRowSkips;

            csvReader = new LumenWorks.Framework.IO.Csv.CsvReader(this.textReader, hasHeaders, delimeter);
            maxFieldCount = csvReader.FieldCount;

            Configure();
        }

        private void Configure()
        {
            csvReader.DefaultParseErrorAction = ParseErrorAction.RaiseEvent;
            csvReader.ParseError += Reader_ParseError;
            csvReader.SkipEmptyLines = true;
        }

        private void Reader_ParseError(object? sender, ParseErrorEventArgs e)
        {
            logger.Log("Error when reading a line in the file, skip to the next line");
            logger.Log(e.ToString());
            e.Action = ParseErrorAction.AdvanceToNextLine;
        }

        public string[] GetCurrentValuesRecord()
        {
            string[] result = new string[maxFieldCount];

            for (int i = 0; i < maxFieldCount; i++)
            {
                result[i] = csvReader[i];
            }

            return result;
        }

        public bool ReadNextRecord()
        {
            return Read();
        }

        public void SkipRows(int countRow)
        {
            for (var i = 0; i < countRow; i++)
            {
                Read();
            }
        }

        private bool Read()
        {
            return csvReader.ReadNextRecord();
        }

        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    csvReader.Dispose();
                    textReader.Dispose();
                }

                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CsvFileReader()
        {
            Dispose(false);
        }
    }
}
