using System.Collections.Generic;
using System.Security.AccessControl;

namespace ProductCatalog.Domain.ViewModels
{
    public class PaginatedItemsViewModel<TEntity> where TEntity : class
    {
        public int PageSize { get; }
        public int PageIndex { get; }
        public long Count { get; }
        public IEnumerable<TEntity> Items { get; }

        public PaginatedItemsViewModel(int pageSize, int pageIndex, long count, IEnumerable<TEntity> items)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            Count = count;
            Items = items;  
        }
    }
}
