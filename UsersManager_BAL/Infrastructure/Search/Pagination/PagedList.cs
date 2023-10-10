namespace UsersManager_BAL.Infrastructure.Search.Pagination
{
    [Serializable]
    public class PagedList<T> : List<T>, IPagedList<T>
    {
        public PagedList()
        {

        }

        private PagedList(int totalCount, PagedListFilter filter = default)
        {
            Filter = filter ?? PagedListFilter.Default;
            TotalCount = totalCount;
        }

        public PagedList(IQueryable<T> source, PagedListFilter filter = default, bool getOnlyTotalCount = false) : this(source.Count(), filter)
        {
            if (getOnlyTotalCount)
                return;
            AddRange(source.Skip((Filter.Page - 1) * Filter.PageSize).Take(Filter.PageSize).ToList());
        }

        public PagedList(ICollection<T> source, PagedListFilter filter = default) : this(source.Count, filter)
        {
            AddRange(source.Skip((Filter.Page - 1) * Filter.PageSize).Take(Filter.PageSize).ToList());
        }

        public PagedList(IEnumerable<T> source, int totalCount, PagedListFilter filter = default) : this(totalCount, filter)
        {
            AddRange(source);
        }

        public PagedListFilter Filter { get; set; }

        public int TotalCount { get; }
    }
}
