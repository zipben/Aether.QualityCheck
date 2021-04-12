using System.Collections.Generic;
using System.Linq;

namespace Aether.Extensions
{
    public static class EnumerableExtensions
    {
        public static bool HasMoreThanOneItem<T>(this IEnumerable<T> enumeration) =>
            enumeration != null && enumeration.Any() && enumeration.Skip(1).Any();
    }
}
