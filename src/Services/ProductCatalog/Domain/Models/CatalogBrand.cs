using System.Collections.Generic;

namespace ProductCatalog.Domain.Models
{
    public class CatalogBrand
    {
        public int Id { get; set; }
        public string Brand { get; set; }

        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
