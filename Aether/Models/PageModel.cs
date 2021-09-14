using System.Collections.Generic;

namespace Aether.Models
{
    public class PageModel<T> where T: class
    {
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }
        public bool IsLastPage { get; set; }
        public PageParams PageParams { get; set; }

        public PageModel(List<T> data = null, long ? totalCount = null, PageParams pageParams = null)
        {
            Data = data is null ? new List<T>() : data;
            TotalCount = totalCount ?? 0;
            PageParams = pageParams ?? new PageParams();
        }
    }
}
