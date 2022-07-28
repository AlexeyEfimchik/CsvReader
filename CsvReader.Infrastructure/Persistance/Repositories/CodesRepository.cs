using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;

namespace CsvReader.Infrastructure.Persistance.Repositories
{
    public class CodesRepository : IRepository<Code>
    {
        private BusinessProcessesContext context;

        public CodesRepository(BusinessProcessesContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(IEnumerable<Code> values)
        {
            this.context.AddRange(values);
        }
    }
}
