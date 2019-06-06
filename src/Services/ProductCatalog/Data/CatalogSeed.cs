using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProductCatalog.Domain.Models;

namespace ProductCatalog.Data
{
    public class CatalogSeed
    {
        public static async Task SeedTestData(CatalogContext context)
        {
            if (!context.CatalogTypes.Any())
            {
                context.CatalogTypes.AddRange(GenerateCatalogTypes());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogBrands.Any())
            {
                context.CatalogBrands.AddRange(GenerateCatalogBrands());
                await context.SaveChangesAsync();
            }

            if (!context.CatalogItems.Any())
            {
                context.CatalogItems.AddRange(GenerateCatalogItems());
                await context.SaveChangesAsync();
            }
        }

        static IEnumerable<CatalogItem> GenerateCatalogItems()
        {
            return new List<CatalogItem>
            {
                new CatalogItem
                {
                    Name = "Air",
                    Description = "Super Air",
                    PictureFileName = "shoes-1.jpg",
                    PictureUrl = "https://externalcatalogurltoreplace/api/Pic/1",
                    Price = 199.50M,
                    CatalogBrandId = 1,
                    CatalogTypeId = 1
                },

                new CatalogItem
                {
                    Name = "Land",
                    Description = "Super Land",
                    PictureFileName = "shoes-2.jpg",
                    PictureUrl = "https://externalcatalogurltoreplace/api/Pic/2",
                    Price = 299.50M,
                    CatalogBrandId = 2,
                    CatalogTypeId = 2
                },

                new CatalogItem
                {
                    Name = "Water",
                    Description = "Super water",
                    PictureFileName = "shoes-3.jpg",
                    PictureUrl = "https://externalcatalogurltoreplace/api/Pic/3",
                    Price = 159.50M,
                    CatalogBrandId = 3,
                    CatalogTypeId = 3
                }
            };
        }

        static IEnumerable<CatalogType> GenerateCatalogTypes()
        {
            return new List<CatalogType>
            {
                new CatalogType { Type = "Running" },
                new CatalogType { Type = "Basketball" },
                new CatalogType { Type = "Tennis" }
            };
        }

        static IEnumerable<CatalogBrand> GenerateCatalogBrands()
        {
            return new List<CatalogBrand>
            {
                new CatalogBrand { Brand = "Adidas" },
                new CatalogBrand { Brand = "Puma" },
                new CatalogBrand { Brand = "Reebok" }
            };
        }
    }
}
