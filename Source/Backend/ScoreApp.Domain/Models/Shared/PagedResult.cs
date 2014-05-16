using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public class PagedResult<T> : PagedResult
    {
        public IEnumerable<T> Items { get; set; }
    }

    public class PagedResult
    {
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
    }
}
