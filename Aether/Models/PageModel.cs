using System.Collections.Generic;

namespace Aether.Models
{
    public class PageModel<T> where T: class
    {
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }

        public PageModel(List<T> data = null, long ? totalCount = null)
        {
            Data = data is null ? new List<T>() : data;
            TotalCount = totalCount ?? 0;
        }
    }
}
