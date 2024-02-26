using Domain.Entities.RulesAggregate;

namespace Application.Helper
{
    internal class RuleActionComparer : IEqualityComparer<RuleAction>
    {
        public bool Equals(RuleAction x, RuleAction y)
        {
            // Check for null values
            if (x == null || y == null)
                return false;

            // Check if the two Person objects are the same reference
            if (ReferenceEquals(x, y))
                return true;

            // Compare the SSN of the two Person objects
            // to determine if they're the same
            return x.Id == y.Id;
        }

        public int GetHashCode(RuleAction? obj)
        {
            if (obj == null || obj.Id == null)
                return 0;

            // Use the SSN of the Person object
            // as the hash code
            return obj.Id.GetHashCode();
        }
    }
}
