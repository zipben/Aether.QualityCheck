using System.Collections.Generic;

namespace Aether.Models
{
    public class PageModel<T> where T: class
    {
        public List<T> Data { get; set; }
        public long TotalCount { get; set; }
    }
}
