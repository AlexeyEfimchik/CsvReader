using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;

namespace CsvReader.Infrastructure.Persistance.Repositories
{
    public class ProcessRepository : IRepository<Process>
    {
        private BusinessProcessesContext context;

        public ProcessRepository(BusinessProcessesContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(IEnumerable<Process> values)
        {
            this.context.AddRange(values);
        }
    }
}
