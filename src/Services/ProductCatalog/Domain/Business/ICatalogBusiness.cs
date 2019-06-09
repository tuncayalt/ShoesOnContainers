using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using ProductCatalog.Data;
using ProductCatalog.Domain.Models;
using ProductCatalog.Domain.ViewModels;

namespace ProductCatalog.Domain.Business
{
    public interface ICatalogBusiness
    {
        Task<CatalogItem> GetCatalogItemById(CatalogContext context, IOptionsSnapshot<CatalogSettings> settings, int id);
        Task<IEnumerable<CatalogBrand>> CatalogBrands(CatalogContext context);
        Task<IEnumerable<CatalogType>> CatalogTypes(CatalogContext context);

        Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItems(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            int pageSize,
            int pageIndex);

        Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItemsByName(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            string name,
            int pageSize,
            int pageIndex);

        Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItemsByTypeAndBrand(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            int? catalogTypeId,
            int? catalogBrandId,
            int pageSize,
            int pageIndex);

        Task<int?> UpdateProduct(CatalogContext context, CatalogItem catalogItemToUpdate);
        Task<int?> CreateProduct(CatalogContext context, CatalogItem catalogItemToCreate);
        Task<int?> DeleteProduct(CatalogContext context, int id);
    }
}
