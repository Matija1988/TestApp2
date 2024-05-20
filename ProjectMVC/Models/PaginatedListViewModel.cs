using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using ProjectService.Model;

namespace ProjectMVC.Models
{
    public class PaginatedListViewModel<T>
    {
        public List<T> Items { get; set; }

        public int TotalItems { get; set; }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public int TotalPages { get; private set; }

        public int OrderNumber { get; set; }

        public PaginatedListViewModel(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public bool HasPreviousPage => (PageIndex > 1);

        public bool HasNextPage => (PageIndex < TotalPages);    

        public int FirstItemIndex => (PageIndex -1) * PageSize +1;

        public int LastItemIndex => Math.Min(PageIndex * PageSize, TotalPages);

        public static async Task<PaginatedListViewModel<T>> Paginate(List<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();

            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            PaginatedListViewModel<T> paginatedList =  new PaginatedListViewModel<T>(items, count, pageIndex, pageSize); ;

            return paginatedList; 

        }

    }
}
