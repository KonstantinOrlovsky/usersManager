using UsersManager_BAL.Infrastructure.Search.Pagination;
using UsersManager_BAL.Infrastructure.Search.Sorting;

namespace UsersManager_BAL.Models
{
    public class CommonFilterModel<T> where T : new()
    {
        public T SearchFilter { get; set; } = new T();
        public PagedListFilter Filter { get; set; } = new PagedListFilter();
        public SortedBy SortingBy { get; set; } = new SortedBy();
    }
}
