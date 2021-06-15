using System;
using System.Collections.Generic;

namespace HttpClients.Models
{
    public class PageModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int PageIndex { get; set; }
        
        public int TotalPages { get; set; }

        public int StartPage { get; set; }

        public int LastPage { get; set; }

        public int PageSize { get; set; }

        public PageModel(int count, int? pageIndex, int? pageSize, IEnumerable<T> items)
        {
            PageIndex = pageIndex ?? 1;

            if (pageSize == null)
            {
                pageSize = 3;
            }

            PageSize = pageSize.Value;

            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
            int startPage = PageIndex - 2;
            int lastPage = PageIndex + 2;

            if (startPage <=0)
            {
                lastPage = lastPage - (startPage - 1);
                startPage = 1;
            }

            if (lastPage > TotalPages)
            {
                lastPage = TotalPages;
                if (lastPage > 5)
                {
                    startPage = lastPage - 4;
                }
            }

            StartPage = startPage;
            LastPage = lastPage;
        }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPages);
            }
        }
    }
}
