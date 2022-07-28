namespace CsvReader.Domain.Entities
{
    public class Process : BaseAuditableEntity, IAggregateRoot
    {
        public string Name { get; set; }
        public int BusinessProcessId { get; set; }
        public List<BusinessProcess> BusinessProcess { get; set; }
    }
}
