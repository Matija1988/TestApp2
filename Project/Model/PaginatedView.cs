using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace ProjectService.Model
{
    public class PaginatedView<T>
    {
        public List<T> Source { get; set; }

        public int TotalItems { get; set; }

        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; private set; }



        public PaginatedView(List<T> source, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            Source = source;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);

        public int FirstItemIndex => (PageIndex -1) * PageSize + 1;

        public int LastItemIndex => Math.Min(PageIndex * PageSize, TotalItems);

        public static async Task<PaginatedView<T>> PaginateAsync(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count;

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new PaginatedView<T>(items, count, pageIndex, pageSize);
          
        }

    }

}
