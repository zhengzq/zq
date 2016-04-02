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

        public static List<Dictionary<string, object>> Serialize(this IPagedList<dynamic> page)
        {
            var list = new List<Dictionary<string, object>>();
            foreach (var o in page)
            {
                var result = new Dictionary<string, object>();
                var dictionary = o as IDictionary<string, object>;
                if (dictionary != null)
                    foreach (var item in dictionary)
                    {
                        result.Add(item.Key, item.Value);
                    }
                list.Add(result);
            }
            return list;
        }
    }
}
