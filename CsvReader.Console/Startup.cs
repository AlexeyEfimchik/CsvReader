using CsvReader.Application.Common.Intrefaces;
using CsvReader.Application.Common.Utils;
using CsvReader.Application.Services;
using CsvReader.Console.Interfaces;
using CsvReader.Console.Settings;
using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;
using CsvReader.Infrastructure.Persistance.Repositories;
using Microsoft.Extensions.Configuration;
using System.Text;

namespace CsvReader.Console
{
    public class Startup
    {
        private ISettings settings;
        private ILogger logger;
        private IBuffer<BusinessProcess> buffer;
        private IUnitOfWork unitOfWork;

        public Startup(IConfiguration configuration)
        {
            this.settings = new ApplicationSettings();
            configuration.Bind("ApplicationSettings", this.settings);

            this.logger = new ConsoleLogger();
            this.buffer = new TransferBuffer<BusinessProcess>(settings.BufferSize);
            this.unitOfWork = new UnitOfWork(this.settings.ConnectionDbString);
        }

        private void SetEncoding()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        private void CheckCsvFile()
        {
            if (!File.Exists(this.settings.PathCsv))
            {
                throw new FileNotFoundException(".csv file not found");
            }
        }

        public void Start()
        {
            CheckCsvFile();
            SetEncoding();

            using (TextReader textReader = new StreamReader(this.settings.PathCsv, Encoding.GetEncoding(this.settings.EncodingCsv)))
            using (ICsvFileReader csvReader = new CsvFileReader(textReader, this.settings.HasHeaders, this.settings.Delimeter, logger, this.settings.NumberOfLinesToSkipInTheStartFile))
            using (ICsvToDbTransfer csvToDbTransfer = new CsvToDbTransferService(this.unitOfWork, csvReader, this.buffer, this.logger))
            {
                csvToDbTransfer.Transfer();
            }
        }
    }
}
