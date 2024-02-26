using Domain.Entities.ListAggregate;

namespace Application.Helper
{
    public class ListValueComparer : IEqualityComparer<ListValue>
    {
        public bool Equals(ListValue x, ListValue y)
        {
            // Check for null values
            if (x == null || y == null)
                return false;

            // Check if the two Person objects are the same reference
            if (ReferenceEquals(x, y))
                return true;

            // Compare the SSN of the two Person objects
            // to determine if they're the same
            return x.Key == y.Key;
        }

        public int GetHashCode(ListValue? obj)
        {
            if (obj == null || obj.Key == null)
                return 0;

            // Use the SSN of the Person object
            // as the hash code
            return obj.Key.GetHashCode();
        }
    }
}
