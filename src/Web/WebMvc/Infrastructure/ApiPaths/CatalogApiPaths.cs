using System.Reflection;
using System.Text;

namespace ShoesOnContainers.Web.WebMvc.Infrastructure.ApiPaths
{
    //Todo: should use some kind of service discovery like Consul.io or Eureka

    public static class CatalogApiPaths
    {
        public static string GetCatalogItems(
            string baseUri,
            int? typeId,
            int? brandId,
            int pageSize = 6,
            int pageIndex = 0)
        {
            //[Route("Items/type/{catalogTypeId}/brand/{catalogBrandId}")]

            var filterBuilder = new StringBuilder();
            if (typeId.HasValue || brandId.HasValue)
            {
                var typeFilter = typeId.HasValue
                    ? typeId.ToString()
                    : "null";
                var brandFilter = brandId.HasValue
                    ? brandId.ToString()
                    : "null";
                filterBuilder.Append($"/type{typeFilter}/brand/{brandFilter}");
            }

            return $"{baseUri}api/Catalog/Items{filterBuilder}?pageSize={pageSize}&pageIndex={pageIndex}";
        }

        public static string GetCatalogItemById(string baseUri, int id)
        {
            //[Route("Items/{id:int}")]

            return $"{baseUri}api/Catalog/Items/{id}";
        }

        public static string GetCatalogTypes(string baseUri)
        {
            return $"{baseUri}api/Catalog/CatalogTypes";
        }

        public static string GetCatalogBrands(string baseUri)
        {
            return $"{baseUri}api/Catalog/CatalogBrands";
        }
    }
}
