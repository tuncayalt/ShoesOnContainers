using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShoesOnContainers.Web.WebMvc.ViewModels.CatalogViewModels
{
    public class PaginationInfo
    {
        public int ItemsPerPage { get; set; }
        public int TotalItems { get; set; }
        public int ActualPage { get; set; }
        public int TotalPages { get; set; }
        public string Previous { get; set; }
        public string Next { get; set; }
    }
}
