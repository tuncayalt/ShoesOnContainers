using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ShoesOnContainers.Web.WebMvc.Models;
using ShoesOnContainers.Web.WebMvc.Services.CatalogServices;
using ShoesOnContainers.Web.WebMvc.ViewModels.CatalogViewModels;

namespace ShoesOnContainers.Web.WebMvc.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _catalogService;

        public CatalogController(ICatalogService catalogService)
        {
            _catalogService = catalogService;
        }

        public async Task<IActionResult> Index(int? brandFilterApplied, int? typeFilterApplied, int? pageIndex)
        {
            const int ItemPage = 10;

            var catalogItems =
                await _catalogService.GetCatalogItems(pageIndex ?? 0, ItemPage, brandFilterApplied, typeFilterApplied);

            var vm = new CatalogIndexViewModel
            {
                CatalogItems = catalogItems.Items,
                BrandFilterApplied = brandFilterApplied ?? 0,
                TypeFilterApplied = typeFilterApplied ?? 0,
                Brands = await _catalogService.GetBrands(),
                Types = await _catalogService.GetTypes(),
                PaginationInfo = new PaginationInfo
                {
                    ActualPage = pageIndex ?? 0,
                    ItemsPerPage = ItemPage,
                    TotalItems = catalogItems.Count,
                    TotalPages = (int)Math.Ceiling((decimal)catalogItems.Count / ItemPage)
                }
            };

            vm.PaginationInfo.Previous = vm.PaginationInfo.ActualPage == 0
                ? "is-disabled"
                : "";

            vm.PaginationInfo.Next = vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1
                ? "is-disabled"
                : "";

            return View(vm);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
