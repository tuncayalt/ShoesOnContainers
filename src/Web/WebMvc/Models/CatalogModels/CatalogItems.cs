using System.Collections.Generic;

namespace ShoesOnContainers.Web.WebMvc.Models.CatalogModels
{
    public class CatalogItems
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public List<CatalogItem> Items { get; set; }
    }
}
