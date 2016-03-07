using System.Collections.Generic;
using System.Linq;

namespace Zq.Paging
{
    public class PagedList<T> : BasePagedList<T>
    {
        public PagedList(IEnumerable<T> superset, int pageIndex, int pageSize)
            : this(superset?.AsQueryable() ?? new List<T>().AsQueryable(), pageIndex, pageSize)
        {
        }

        private PagedList(IQueryable<T> superset, int pageIndex, int pageSize)
            : base(pageIndex, pageSize, superset.Count())
        {
            if (TotalItemCount > 0)
            {
                if (pageIndex < 0)
                {
                    AddRange(superset.Take(pageSize).ToList());
                }
                else
                {
                    if (pageIndex > PageCount)
                        pageIndex = PageCount;

                    var minRowCount = (pageIndex - 1) * pageSize;
                    AddRange(superset.Skip(minRowCount).Take(pageSize).ToList());
                }
            }
        }
    }
}
