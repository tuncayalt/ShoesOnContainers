using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesOnContainers.Web.WebMvc.Models.CatalogModels;

namespace ShoesOnContainers.Web.WebMvc.Services.CatalogServices
{
    public interface ICatalogService
    {
        Task<CatalogItem> GetCatalogItemById(int id);
        Task<CatalogItems> GetCatalogItems(int pageSize, int pageIndex, int? brandId, int? typeId);
        Task<IEnumerable<SelectListItem>> GetBrands();
        Task<IEnumerable<SelectListItem>> GetTypes();
    }
}
