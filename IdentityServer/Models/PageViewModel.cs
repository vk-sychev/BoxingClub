using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Models
{
    public class PageViewModel<T> where T : class
    {
        public IEnumerable<T> Items { get; set; }

        public int PageIndex { get; private set; }
        
        public int TotalPages { get; private set; }

        public int StartPage { get; private set; }

        public int LastPage { get; private set; }

        public PageViewModel(int count, int? pageIndex, int? pageSize, IEnumerable<T> items)
        {
            PageIndex = pageIndex ?? 1;

            if (pageSize == null)
            {
                pageSize = 3;
            }

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
