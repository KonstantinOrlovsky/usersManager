namespace UsersManager_BAL.Infrastructure.Search.Pagination
{
    public static class PaginationExtensions
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> items, PagedListFilter filter = default,
            bool getOnlyTotalCount = false) => new PagedList<T>(items, filter, getOnlyTotalCount);

        public static PagedList<TResult> Select<T, TResult>(this IPagedList<T> items, Func<T, TResult> selector) =>
            new PagedList<TResult>(((IEnumerable<T>)items).Select(selector), items.TotalCount, items.Filter);
    }
}