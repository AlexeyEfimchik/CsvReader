using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;
using CsvReader.Infrastructure.Persistance.Interceptors;
using CsvReader.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace CsvReader.Infrastructure.Persistance.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private BusinessProcessesContext context;

        private IRepository<Code> codeRepository;
        private IRepository<Process> processRepository;
        private IRepository<Division> divisionRepository;
        private IRepository<BusinessProcess> businessProcessRepository;
        private bool disposed;

        public UnitOfWork(string connectionString)
        {
            var options = new DbContextOptionsBuilder<BusinessProcessesContext>().UseSqlServer(connectionString).Options;

            this.context = new BusinessProcessesContext(options, new AuditableEntitySaveChangesInterceptor(new DateTimeService()));
            this.processRepository = new ProcessRepository(this.context);
            this.divisionRepository = new DivisionRepository(this.context);
            this.businessProcessRepository = new BusinessProcessRepository(this.context);
            this.codeRepository = new CodesRepository(this.context);
        }
        public IRepository<Code> Codes => codeRepository;
        public IRepository<Division> Divisions => divisionRepository;
        public IRepository<Process> Processes => processRepository;
        public IRepository<BusinessProcess> BusinessProcesses => businessProcessRepository;

        public void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    this.context.Dispose();
                }

                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        ~UnitOfWork()
        {
            Dispose(false);
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
