namespace CsvReader.Domain.Common
{
    public interface IRepository<T> where T : IAggregateRoot
    {
        public void AddRange(IEnumerable<T> values);
    }
}
