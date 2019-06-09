using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ProductCatalog.Data;
using ProductCatalog.Domain.Business;
using ProductCatalog.Domain.Models;

namespace ProductCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IOptionsSnapshot<CatalogSettings> _settings;
        private readonly ICatalogBusiness _business;
        private readonly ILogger<CatalogController> _logger;

        public CatalogController(CatalogContext context, IOptionsSnapshot<CatalogSettings> settings, [FromServices] ICatalogBusiness business, [FromServices] ILogger<CatalogController> logger)
        {
            _context = context;
            _settings = settings;
            _business = business;
            _logger = logger;
            //context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogBrands()
        {
            try
            {
                var brands = await _business.CatalogBrands(_context);
                return Ok(brands);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(CatalogBrands)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> CatalogTypes()
        {
            try
            {
                var types = await _business.CatalogTypes(_context);
                return Ok(types);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(CatalogTypes)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("Items/{id:int}")]
        public async Task<IActionResult> GetCatalogItemById(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var item = await _business.GetCatalogItemById(_context, _settings, id);

                if (item == null)
                {
                    return NotFound();
                }

                return Ok(item);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCatalogItemById)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/Catalog/Items[?pageSize=4&pageIndex=3]
        [HttpGet]
        [Route("Items")]
        public async Task<IActionResult> GetCatalogItems(
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            try
            {
                if (pageSize < 0 || pageIndex < 0)
                {
                    return BadRequest();
                }

                var catalogItemsViewModel = await _business.GetCatalogItems(_context, _settings, pageSize, pageIndex);

                return Ok(catalogItemsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCatalogItems)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/Catalog/Items/withname/a[?pageSize=4&pageIndex=3]
        [HttpGet]
        [Route("Items/withname/{name:minlength(1)}")]
        public async Task<IActionResult> GetCatalogItemsByName(
            string name,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            try
            {
                if (pageSize < 0 || pageIndex < 0)
                {
                    return BadRequest();
                }

                var catalogItemsViewModel = await _business.GetCatalogItemsByName(_context, _settings, name, pageSize, pageIndex);

                return Ok(catalogItemsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCatalogItemsByName)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //GET api/Catalog/Items/type/1/brand/null/[?pageSize=4&pageIndex=3]
        [HttpGet]
        [Route("Items/type/{catalogTypeId}/brand/{catalogBrandId}")]
        public async Task<IActionResult> GetCatalogItemsByTypeAndBrand(
            int? catalogTypeId,
            int? catalogBrandId,
            [FromQuery] int pageSize = 6,
            [FromQuery] int pageIndex = 0)
        {
            try
            {
                if (pageSize < 0 || pageIndex < 0)
                {
                    return BadRequest();
                }

                var catalogItemsViewModel =
                    await _business.GetCatalogItemsByTypeAndBrand(_context, _settings, catalogTypeId, catalogBrandId, pageSize, pageIndex);

                return Ok(catalogItemsViewModel);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(GetCatalogItemsByTypeAndBrand)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("Items")]
        public async Task<IActionResult> CreateProduct([FromBody] CatalogItem catalogItemToCreate)
        {
            try
            {
                var createdItemId = await _business.CreateProduct(_context, catalogItemToCreate);
                if (createdItemId == null)
                {
                    return BadRequest("Cannot create item.");
                }

                return CreatedAtAction(nameof(GetCatalogItemById), new { id = createdItemId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(CreateProduct)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPut]
        [Route("Items")]
        public async Task<IActionResult> UpdateProduct([FromBody] CatalogItem catalogItemToUpdate)
        {
            try
            {
                var updatedItemId = await _business.UpdateProduct(_context, catalogItemToUpdate);
                if (updatedItemId == null)
                {
                    return NotFound($"Cannot find product with id: {catalogItemToUpdate.Id}");
                }

                return CreatedAtAction(nameof(GetCatalogItemById), new { id = updatedItemId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(UpdateProduct)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpDelete]
        [Route("Items/{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest();
                }

                var deletedItemId = await _business.DeleteProduct(_context, id);

                if (deletedItemId == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error during {nameof(DeleteProduct)}");
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}