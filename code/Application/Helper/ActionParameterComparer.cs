using Domain.Entities.RulesAggregate;

namespace Application.Helper
{
    public class ActionParameterComparer : IEqualityComparer<ActionParameter>
    {
        public bool Equals(ActionParameter x, ActionParameter y)
        {
            // Check for null values
            if (x == null || y == null)
                return false;

            // Check if the two Person objects are the same reference
            if (ReferenceEquals(x, y))
                return true;

            // Compare the SSN of the two Person objects
            // to determine if they're the same
            return x.Name == y.Name;
        }

        public int GetHashCode(ActionParameter? obj)
        {
            if (obj == null || obj.Id == null)
                return 0;

            // Use the SSN of the Person object
            // as the hash code
            return obj.Name.GetHashCode();
        }
    }
}
