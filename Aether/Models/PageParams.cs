namespace Aether.Models
{
    public class PageParams
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public bool Descending { get; set; }
    }
}
