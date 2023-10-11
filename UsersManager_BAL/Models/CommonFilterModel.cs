using UsersManager_BAL.Infrastructure.Search.Pagination;
using UsersManager_BAL.Infrastructure.Search.Sorting;

namespace UsersManager_BAL.Models
{
    public class CommonFilterModel<TEntity> where TEntity : new()
    {
        public TEntity SearchFilter { get; set; } = new TEntity();
        public PagedListFilter Filter { get; set; } = new PagedListFilter();
        public SortedBy SortingBy { get; set; } = new SortedBy();
    }
}