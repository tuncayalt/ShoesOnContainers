namespace ShoesOnContainers.Web.WebMvc.Models.CatalogModels
{
    public class CatalogItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PictureUrl { get; set; }
        public string PictureFileName { get; set; }
        public decimal Price { get; set; }

        public int CatalogTypeId { get; set; }
        public string CatalogType { get; set; }
        public int CatalogBrandId { get; set; }
        public string CatalogBrand { get; set; }
    }
}
