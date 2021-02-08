using System.Collections.Generic;
using Aether.Interfaces;

namespace Aether.Extensions
{
    public static class DataElementClassificationExtensions
    {
        public static object MakeObjectToLog(this IDataElementClassification dataElementClassification) =>
            new
            {
                dataElementClassification.Category,
                dataElementClassification.Classification,
                dataElementClassification.Entity,
                dataElementClassification.HasSSN
            };

        public static object MakeObjectToLog(this IEnumerable<IDataElementClassification> dataElementClassificationList)
        {
            var list = new List<object>();
            foreach (var item in dataElementClassificationList)
            {
                list.Add(item.MakeObjectToLog());
            }
            return list;
        }
    }
}
