using System.Collections.Generic;

namespace Aether.Models
{
    public class PageModel<T> where T: class
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
