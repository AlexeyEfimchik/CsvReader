using CsvReader.Domain.Entities;

namespace CsvReader.Domain.Common
{
    public interface IUnitOfWork : IDisposable
    {
        public IRepository<Code> Codes { get; }
        public IRepository<Division> Divisions { get; }
        public IRepository<Process> Processes { get; }
        public IRepository<BusinessProcess> BusinessProcesses { get; }

        public int SaveChanges();

    }
}
