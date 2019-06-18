using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ShoesOnContainers.Web.WebMvc.Infrastructure.ApiPaths;
using ShoesOnContainers.Web.WebMvc.Infrastructure.HttpClients;
using ShoesOnContainers.Web.WebMvc.Models.CatalogModels;

namespace ShoesOnContainers.Web.WebMvc.Services.CatalogServices
{
    public class CatalogService : ICatalogService
    {
        private readonly IOptionsSnapshot<AppSettings> _settings;
        private readonly IHttpClient _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public CatalogService(IOptionsSnapshot<AppSettings> settings, IHttpClient httpClient, ILogger<CatalogService> logger)
        {
            _settings = settings;
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<CatalogItem> GetCatalogItemById(int id)
        {
            var apiPath = CatalogApiPaths.GetCatalogItemById(_settings.Value.CatalogServiceUri, id);

            var catalog = await _httpClient.GetStringAsync(apiPath);

            return JsonConvert.DeserializeObject<CatalogItem>(catalog);
        }

        public async Task<CatalogItems> GetCatalogItems(int pageSize, int pageIndex, int? brandId, int? typeId)
        {
            var apiPath = CatalogApiPaths.GetCatalogItems(
                _settings.Value.CatalogServiceUri,
                typeId,
                brandId,
                pageSize,
                pageIndex);

            var catalogItems = await _httpClient.GetStringAsync(apiPath);

            return JsonConvert.DeserializeObject<CatalogItems>(catalogItems);
        }

        public async Task<IEnumerable<SelectListItem>> GetBrands()
        {
            var apiPath = CatalogApiPaths.GetCatalogBrands(_settings.Value.CatalogServiceUri);

            var brandsString = await _httpClient.GetStringAsync(apiPath);

            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }
            };

            var brands = JArray.Parse(brandsString);

            foreach (var brand in brands.Children<JObject>())
            {
                items.Add(new SelectListItem
                {
                    Value = brand.Value<string>("id"),
                    Text = brand.Value<string>("brand")
                });
            }

            return items;
        }

        public async Task<IEnumerable<SelectListItem>> GetTypes()
        {
            var apiPath = CatalogApiPaths.GetCatalogTypes(_settings.Value.CatalogServiceUri);

            var typesString = await _httpClient.GetStringAsync(apiPath);

            var items = new List<SelectListItem>
            {
                new SelectListItem
                {
                    Value = null,
                    Text = "All",
                    Selected = true
                }
            };

            var types = JArray.Parse(typesString);

            foreach (var type in types.Children<JObject>())
            {
                items.Add(new SelectListItem
                {
                    Value = type.Value<string>("id"),
                    Text = type.Value<string>("type")
                });
            }

            return items;
        }
    }
}
