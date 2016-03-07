using System.Collections.Generic;

namespace Zq.Paging
{
    public static class PagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> superset, int index, int pageSize)
        {
            return new PagedList<T>(superset, index, pageSize);
        }
        public static IPagedList<dynamic> ToPageDynamicList<dynamic>(this IEnumerable<dynamic> superset, int index, int pageSize)
        {
            return new PagedList<dynamic>(superset, index, pageSize);
        }
    }
}
