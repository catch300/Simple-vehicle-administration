using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_service.PagingFIlteringSorting
{
    public abstract class IPaginatedList<T> : List<T>
    {
        public int? PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Data { get; set; }
        public new int Count { get; set; }
        public IPaginatedList( )
        {

        }

        protected IPaginatedList(IEnumerable<T> items, int count, int? pageIndex, int pageSize)
        {
            Data = items;
            PageIndex = pageIndex;
            PageSize = pageSize;
            Count = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            AddRange(items);
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

        public abstract Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize);
        
    }
}
