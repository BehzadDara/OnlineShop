using Catalog.Api.Entities;
using Catalog.Api.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Api.Controllers
{
    [Route("api/v1/[controller]/[action]")]
    [ApiController]
    public class ProductController(IProductRepository _productRepository, ILogger<ProductController> _logger) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetAll()
        {
            var result = await _productRepository.GetAll();
            return Ok(result);
        }

        [HttpGet("{id:length(24)}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetById(string id)
        {
            var result = await _productRepository.GetById(id);
            if (result is null)
            {
                _logger.LogError($"product with id: {id} not found.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{name}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetByName(string name)
        {
            var result = await _productRepository.GetByName(name);
            if (result is null)
            {
                _logger.LogError($"product with name: {name} not found.");
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{category}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetByCategory(string category)
        {
            var result = await _productRepository.GetByCategory(category);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Create([FromBody] Product product)
        {
            await _productRepository.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id}, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Update([FromBody] Product product)
        {
            var result = await _productRepository.Update(product);
            return Ok(result);
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> Delete(string id)
        {
            var result = await _productRepository.Delete(id);
            return Ok(result);
        }
    }
}
