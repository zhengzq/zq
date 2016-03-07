using System;
using System.Collections.Generic;

namespace Zq.Paging
{
    public abstract class BasePagedList<T> : List<T>, IPagedList<T>
    {
        protected internal BasePagedList(int index, int pageSize, int totalItemCount)
        {
            PageSize = pageSize;
            PageIndex = index;
            TotalItemCount = totalItemCount;

            if (TotalItemCount > 0)
                PageCount = (int)Math.Ceiling(TotalItemCount / (double)PageSize);
            else
                PageCount = 0;
        }

        #region IPagedList<T> Members

        public int PageCount { get; protected set; }

        public int TotalItemCount { get; protected set; }

        public int PageIndex { get; protected set; }

        public int PageNumber
        {
            get { return PageIndex + 1; }
        }

        public int PageSize { get; protected set; }

        public bool HasPreviousPage
        {
            get { return PageIndex > 0; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < (PageCount - 1); }
        }

        public bool IsFirstPage
        {
            get { return PageIndex <= 0; }
        }

        public bool IsLastPage
        {
            get { return PageIndex >= (PageCount - 1); }
        }

        #endregion

    }
}
