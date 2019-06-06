using System.Collections.Generic;

namespace ProductCatalog.Domain.Models
{
    public class CatalogType
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<CatalogItem> CatalogItems { get; set; }
    }
}
