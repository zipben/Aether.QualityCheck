using System;
using System.Collections.Generic;
using System.Linq;

namespace Aether.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Breaks a list into sub-lists of a certain size and returns them all in as a list of those lists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="originalList"></param>
        /// <param name="batchSize"></param>
        /// <returns></returns>
        public static List<List<T>> Batch<T>(this List<T> originalList, int batchSize)
        {
            if (originalList == null)
                throw new ArgumentNullException(nameof(originalList));

            if (batchSize == 0)
                return new List<List<T>>() { originalList };

            List<List<T>> batchedItems = new List<List<T>>();

            while (originalList.Any())
            {
                batchedItems.Add(originalList.Take(batchSize).ToList());

                originalList = originalList.Skip(batchSize).ToList();
            }

            return batchedItems;
        }

        /// <summary>
        /// Create a one-item list out of the item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <returns></returns>
        public static List<T> CreateList<T>(this T item) =>
            new List<T> { item };

        /// <summary>
        /// Add string to the collection if it is not null nor whitespace
        /// </summary>
        /// <param name="list"></param>
        /// <param name="item"></param>
        public static void AddIfNotNull(this ICollection<string> list, string item)
        {
            if (item.Exists())
            {
                list.Add(item);
            }
        }
    }
}
