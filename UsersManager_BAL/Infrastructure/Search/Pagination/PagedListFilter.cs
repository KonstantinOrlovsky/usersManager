using System.ComponentModel;

namespace UsersManager_BAL.Infrastructure.Search.Pagination
{
    public class PagedListFilter
    {
        public static PagedListFilter Default = new PagedListFilter { _page = 1, _pageSize = 10 };

        private int _page;
        [DefaultValue(1)]
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? Default.Page : value;
        }

        private int _pageSize;
        [DefaultValue(10)]
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? Default.PageSize : value;
        }
    }
}