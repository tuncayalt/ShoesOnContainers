using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShoesOnContainers.Web.WebMvc.Models.CatalogModels;

namespace ShoesOnContainers.Web.WebMvc.ViewModels.CatalogViewModels
{
    public class CatalogIndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
        public IEnumerable<SelectListItem> Types { get; set; }
        public IEnumerable<SelectListItem> Brands { get; set; }
        public int? TypeFilterApplied { get; set; }
        public int? BrandFilterApplied { get; set; }
        public PaginationInfo PaginationInfo { get; set; }
    }
}
