using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;

namespace CsvReader.Infrastructure.Persistance.Repositories
{
    public class DivisionRepository : IRepository<Division>
    {
        private BusinessProcessesContext context;

        public DivisionRepository(BusinessProcessesContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(IEnumerable<Division> values)
        {
            this.context.AddRange(values);
        }
    }
}
