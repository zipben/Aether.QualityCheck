using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Aether.Extensions
{
    public static class AsyncEnumerableExtensions
    {
        public static async Task<List<T>> ToListAsync<T>(this IAsyncEnumerable<T> items)
        {
            if (items is null)
            {
                return null;
            }

            var results = new List<T>();
            await foreach (var item in items)
            { 
                results.Add(item);
            }
            return results;
        }

        public static IEnumerable<T> ToEnumerable<T>(this IAsyncEnumerable<T> items) =>
            ToListAsync(items).Result.AsEnumerable();
    }
}
