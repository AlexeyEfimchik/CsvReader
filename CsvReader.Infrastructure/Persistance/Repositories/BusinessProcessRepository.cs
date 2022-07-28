using CsvReader.Domain.Common;
using CsvReader.Domain.Entities;

namespace CsvReader.Infrastructure.Persistance.Repositories
{
    public class BusinessProcessRepository : IRepository<BusinessProcess>
    {
        private BusinessProcessesContext context;

        public BusinessProcessRepository(BusinessProcessesContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public void AddRange(IEnumerable<BusinessProcess> values)
        {
            this.context.AddRange(values);
        }
    }
}
