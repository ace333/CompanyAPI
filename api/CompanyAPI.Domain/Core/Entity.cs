namespace CompanyAPI.Domain.Core
{
    public abstract class Entity
    {
        public int Id { get; set; }


        public override int GetHashCode()
        {
            return (GetType().ToString() + Id).GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            var other = obj as Entity;

            if (ReferenceEquals(other, null))
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (GetType() != other.GetType())
                return false;

            if (Id == 0 || other.Id == 0)
                return false;

            return Id == other.Id;
        }
    }
}
