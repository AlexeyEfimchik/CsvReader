using CsvReader.Application.Common.Intrefaces;

namespace CsvReader.Application.Common.Utils
{
    public class TransferBuffer<T> : IBuffer<T>
    {
        private List<T> buffer;
        private int capacity;
        private bool isFilled;

        public TransferBuffer(int сapacity)
        {
            this.capacity = сapacity;
            this.buffer = new List<T>();
            this.isFilled = false;
        }

        public bool IsFilled
        {
            get
            {
                if (this.isFilled || buffer.Count >= this.capacity)
                {
                    return true;
                }

                return false;
            }

            set
            {
                this.isFilled = value;
            }

        }

        public IEnumerable<T> GetValues => this.buffer;

        public void Add(T value)
        {
            this.buffer.Add(value);

            if (this.buffer.Count >= this.capacity)
            {
                this.isFilled = true;
            }
        }

        public void Clear()
        {
            this.buffer.Clear();
            this.isFilled = false;
        }

        public int Count()
        {
            return this.buffer.Count();
        }
    }
}
