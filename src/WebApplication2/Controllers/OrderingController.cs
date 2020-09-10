using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Catalog.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderingController : ControllerBase
    {
        private readonly IOrderingRepository _repository;
        private readonly ILogger<OrderingController> _logger;

        public OrderingController(IOrderingRepository repository, ILogger<OrderingController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Order>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products.ToList());
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Order>> GetProduct(string id)
        {
            var product = await _repository.GetProduct(id);

            if (product == null)
            {
                _logger.LogError($"Product with id: {id}, hasn't been found in database.");
                return NotFound();
            }

            return Ok(product);
        }

        [Route("[action]/{category}")]
        [HttpGet]        
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Order>>> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductByCategory(category);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Order>> CreateProduct([FromBody] Order product)
        {
            await _repository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Order), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Order value)
        {
            return Ok(await _repository.Update(value));
        }

        [HttpDelete("{id:length(24)}")]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string id)
        {
            return Ok(await _repository.Delete(id));
        }
    }
}
