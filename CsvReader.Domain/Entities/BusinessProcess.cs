namespace CsvReader.Domain.Entities
{
    public class BusinessProcess : BaseAuditableEntity, IAggregateRoot
    {
        public int? DivisionId { get; set; }
        public Division Division { get; set; }

        public int? CodeId { get; set; }
        public Code Code { get; set; }

        public int ProcessId { get; set; }
        public Process Process { get; set; }
    }
}
