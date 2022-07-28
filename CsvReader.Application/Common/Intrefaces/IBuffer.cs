namespace CsvReader.Application.Common.Intrefaces
{
    public interface IBuffer<T>
    {
        public bool IsFilled { get; set; }
        public void Add(T value);
        public void Clear();
        public int Count();
        public IEnumerable<T> GetValues { get; }
    }
}
