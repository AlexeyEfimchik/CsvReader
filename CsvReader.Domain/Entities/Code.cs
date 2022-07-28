namespace CsvReader.Domain.Entities
{
    public class Code : BaseAuditableEntity, IAggregateRoot
    {
        public string StringCode { get; set; }
        public int? BusinessProcessId { get; set; }
        public List<BusinessProcess> BusinessProcess { get; set; }
    }
}
