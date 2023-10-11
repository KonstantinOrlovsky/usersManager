namespace UsersManager_BAL.Infrastructure.Search.Pagination
{
    public interface IPagedList
    {
        PagedListFilter Filter { get; set; }

        int TotalCount { get; }
    }

    public interface IPagedList<T> : IPagedList, IList<T>
    {
    }
}
