using System.Collections.Generic;

namespace ScoreApp.Domain
{
    public interface IPagedResult<out T>
    {
        IEnumerable<T> Items { get; }
        int CurrentPage { get; }
        int ItemsPerPage { get; }
        int TotalItems { get; }
        IPagedResult<TTarget> To<TTarget>(IEnumerable<TTarget> items);
    }

    public class PagedResult<T> : IPagedResult<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int CurrentPage { get; set; }
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }

        public IPagedResult<TTarget> To<TTarget>(IEnumerable<TTarget> items)
        {
            return new PagedResult<TTarget>
            {
                CurrentPage = CurrentPage,
                Items = items,
                ItemsPerPage = ItemsPerPage,
                TotalItems = TotalItems
            };
        }
    }
}
