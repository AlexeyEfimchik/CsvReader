namespace CsvReader.Domain.Common
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        private int? requestedHashCode;
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is BaseEntity))
            {
                return false;
            }

            if (Object.ReferenceEquals(this, obj))
            {
                return true;
            }

            if (this.GetType() != obj.GetType())
            {
                return false;
            }

            return obj.GetHashCode() == GetHashCode();
        }

        public override int GetHashCode()
        {
            if (!requestedHashCode.HasValue)
            {
                // XOR for random distribution
                requestedHashCode = this.Id.GetHashCode() ^ 31;
            }

            return requestedHashCode.Value;
        }
    }
}
