namespace Domain.Interfaces;

public static class IEnumerableExtensions
{
    public static void ForEach<T>(this IEnumerable<T> items, Action<T> action)
    {
        if (items != null && items.Any())
        {
            foreach (T item in items)
            {
                action(item);
            }
        }
    }

    public static void ForEach<T>(this IEnumerable<T> items, Action<T, int> action)
    {
        if (items != null && items.Any())
        {
            int i = 0;
            foreach (T item in items)
            {
                action(item, i++);
            }
        }
    }
}
