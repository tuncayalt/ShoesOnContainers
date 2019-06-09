using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ProductCatalog.Data;
using ProductCatalog.Domain.Models;
using ProductCatalog.Domain.ViewModels;

namespace ProductCatalog.Domain.Business
{
    public class CatalogBusiness : ICatalogBusiness
    {
        public async Task<CatalogItem> GetCatalogItemById(
            CatalogContext context, IOptionsSnapshot<CatalogSettings> settings, int id)
        {
            var catalogItem = await context.CatalogItems.FirstOrDefaultAsync(item => item.Id == id);
            catalogItem.ReplacePicturePlaceholder(settings.Value.CatalogPictureUrlPlaceholder, settings.Value.ExternalCatalogUrl);
            return catalogItem;
        }

        public async Task<IEnumerable<CatalogBrand>> CatalogBrands(CatalogContext context)
        {
            return await context.CatalogBrands.ToListAsync();
        }

        public async Task<IEnumerable<CatalogType>> CatalogTypes(CatalogContext context)
        {
            return await context.CatalogTypes.ToListAsync();
        }

        public async Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItems(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            int pageSize,
            int pageIndex)
        {
            var totalItemsCount = await context.CatalogItems.LongCountAsync();
            var items = await context.CatalogItems
                .OrderBy(ci => ci.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items.ForEach(i => i.ReplacePicturePlaceholder(settings.Value.CatalogPictureUrlPlaceholder, settings.Value.ExternalCatalogUrl));
            
            return new PaginatedItemsViewModel<CatalogItem>(pageSize, pageIndex, totalItemsCount, items);
        }

        public async Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItemsByName(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            string name,
            int pageSize,
            int pageIndex)
        {
            var totalItemsCount = await context.CatalogItems.Where(i => i.Name.StartsWith(name)).LongCountAsync();
            var items = await context.CatalogItems
                .Where(ci => ci.Name.StartsWith(name))
                .OrderBy(ci => ci.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items.ForEach(i => i.ReplacePicturePlaceholder(settings.Value.CatalogPictureUrlPlaceholder, settings.Value.ExternalCatalogUrl));

            return new PaginatedItemsViewModel<CatalogItem>(pageSize, pageIndex, totalItemsCount, items);
        }

        public async Task<PaginatedItemsViewModel<CatalogItem>> GetCatalogItemsByTypeAndBrand(
            CatalogContext context,
            IOptionsSnapshot<CatalogSettings> settings,
            int? catalogTypeId,
            int? catalogBrandId,
            int pageSize,
            int pageIndex)
        {
            var queryable = (IQueryable<CatalogItem>)context.CatalogItems;

            if (catalogTypeId.HasValue)
            {
                queryable = queryable.Where(q => q.CatalogTypeId == catalogTypeId);
            }

            if (catalogBrandId.HasValue)
            {
                queryable = queryable.Where(q => q.CatalogBrandId == catalogBrandId);
            }

            var totalItemsCount = await queryable.LongCountAsync();
            var items = await queryable
                .OrderBy(ci => ci.Name)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToListAsync();

            items.ForEach(i => i.ReplacePicturePlaceholder(settings.Value.CatalogPictureUrlPlaceholder, settings.Value.ExternalCatalogUrl));

            return new PaginatedItemsViewModel<CatalogItem>(pageSize, pageIndex, totalItemsCount, items);
        }

        public async Task<int?> CreateProduct(CatalogContext context, CatalogItem catalogItemToCreate)
        {
            var item = new CatalogItem
            {
                CatalogBrandId = catalogItemToCreate.CatalogBrandId,
                CatalogTypeId = catalogItemToCreate.CatalogTypeId,
                Description = catalogItemToCreate.Description,
                Name = catalogItemToCreate.Name,
                PictureFileName = catalogItemToCreate.PictureFileName,
                PictureUrl = catalogItemToCreate.PictureUrl,
                Price = catalogItemToCreate.Price
            };
            context.CatalogItems.Add(item);
            await context.SaveChangesAsync();
            return item.Id;
        }

        public async Task<int?> UpdateProduct(CatalogContext context, CatalogItem catalogItemToUpdate)
        {
            var catalogItem = await context.CatalogItems.SingleOrDefaultAsync(it => it.Id == catalogItemToUpdate.Id);
            if (catalogItem == null)
            {
                return null;
            }

            catalogItem = catalogItemToUpdate;
            context.CatalogItems.Update(catalogItem);
            await context.SaveChangesAsync();

            return catalogItem.Id;
        }

        public async Task<int?> DeleteProduct(CatalogContext context, int id)
        {
            var itemToDelete = await context.CatalogItems.SingleOrDefaultAsync(it => it.Id == id);
            if (itemToDelete == null)
            {
                return null;
            }

            context.CatalogItems.Remove(itemToDelete);
            await context.SaveChangesAsync();
            return itemToDelete.Id;
        }
    }
}
