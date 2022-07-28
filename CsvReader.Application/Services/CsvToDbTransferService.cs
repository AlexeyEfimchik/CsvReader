using CsvReader.Application.Common.Exceptions;
using CsvReader.Application.Common.Intrefaces;
using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;

namespace CsvReader.Application.Services
{
    public class CsvToDbTransferService : ICsvToDbTransfer
    {
        private IUnitOfWork unitOfWork;
        private ICsvFileReader csvFileReader;
        private IBuffer<BusinessProcess> buffer;
        private ILogger logger;
        private bool disposed;

        private Dictionary<string, Code> codes = new Dictionary<string, Code>();
        private Dictionary<string, Process> processes = new Dictionary<string, Process>();
        private Dictionary<string, Division> divisions = new Dictionary<string, Division>();

        public CsvToDbTransferService(IUnitOfWork unitOfWork, ICsvFileReader csvFileReader, IBuffer<BusinessProcess> buffer, ILogger logger)
        {
            if (csvFileReader.MaxFieldCount != 3)
            {
                throw new CsvToDbTransferException("Incorrect file format. The maximum number of fields in a csv file should be three");
            }

            this.buffer = buffer ?? throw new ArgumentNullException(nameof(buffer));
            this.unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            this.csvFileReader = csvFileReader ?? throw new ArgumentNullException(nameof(csvFileReader));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        private void TransferBufferToDatabase()
        {
            this.unitOfWork.BusinessProcesses.AddRange(this.buffer.GetValues);
            this.unitOfWork.SaveChanges();
            this.buffer.Clear();
        }

        private bool CheckValueRecord(string value)
        {
            return value != null && value != string.Empty;
        }

        private T TryAddValueOtherwiseGet<T>(string valueRecord, IDictionary<string, T> dictionary) where T : class, new()
        {
            T value = null;
            string key = valueRecord.ToLower();

            if (CheckValueRecord(valueRecord) && !dictionary.TryGetValue(key, out value))
            {
                value = new T();
                dictionary.Add(key, value);
            }

            return value;
        }

        private void AddDictionaryToDatabase()
        {
            this.unitOfWork.Codes.AddRange(this.codes.Values.Where(o => o.Id == 0));
            this.unitOfWork.Processes.AddRange(this.processes.Values.Where(o => o.Id == 0));
            this.unitOfWork.Divisions.AddRange(this.divisions.Values.Where(o => o.Id == 0));
            this.unitOfWork.SaveChanges();
        }

        private void AddValuesToTransfer(string[] recordValues)
        {
            Code code = TryAddValueOtherwiseGet<Code>(recordValues[0], this.codes);
            if (code != null) { code.StringCode = recordValues[0]; }

            Process process = TryAddValueOtherwiseGet<Process>(recordValues[1], this.processes);
            if (process != null) { process.Name = recordValues[1]; }

            Division division = TryAddValueOtherwiseGet<Division>(recordValues[2], this.divisions);
            if (division != null) { division.Name = recordValues[2]; }

            if (this.buffer.IsFilled) { TransferBufferToDatabase(); }

            if (process != null)
            {
                buffer.Add(new BusinessProcess() { Code = code, Division = division, Process = process });
            }
        }

        public void Transfer()
        {
            this.csvFileReader.SkipRows(this.csvFileReader.NumberFirstRowSkips); //Skip headers

            while (this.csvFileReader.ReadNextRecord())
            {
                string[] recordValues = this.csvFileReader.GetCurrentValuesRecord();

                this.logger.Log($"{recordValues[0]} | {recordValues[1]} | {recordValues[2]}");

                AddValuesToTransfer(recordValues);
            }

            if (this.buffer.Count() > 0) { TransferBufferToDatabase(); }

            AddDictionaryToDatabase();
        }

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.unitOfWork.Dispose();
                    this.csvFileReader.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~CsvToDbTransferService()
        {
            Dispose(false);
        }
    }

}
