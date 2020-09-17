using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using EventBusRabbitMQ.Common;
using EventBusRabbitMQ.Events;
using EventBusRabbitMQ.Producer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication1.API.Entities;
using WebApplication1.API.Repositories.Interfaces;

namespace WebApplication1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly IProductRepository _repository;
        private readonly ILogger<CatalogController> _logger;
        private readonly EventBusRabbitMQProducer _eventBus;

        public CatalogController(IProductRepository repository, ILogger<CatalogController> logger, EventBusRabbitMQProducer eventBus)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger)); 
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
        }

        [HttpPost("publish")]
        public async Task<IActionResult> PublishMessage()
        {
            try
            {
                _eventBus.PublishBasketCheckout(EventBusConstants.BasketCheckoutQueue, new BasketCheckoutEvent{Country = "Pindi",EmailAddress = "Fahadameen@gmail.com",UserName = "fahadameen"});
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {EventId} from {AppName}", "Basket");
                throw;
            }

            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var products = await _repository.GetProducts();
            return Ok(products.ToList());
        }

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Product>> GetProduct(string id)
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
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<Product>>> GetProductByCategory(string category)
        {
            var product = await _repository.GetProductByCategory(category);
            return Ok(product);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.Created)]
        public async Task<ActionResult<Product>> CreateProduct([FromBody] Product product)
        {
            await _repository.Create(product);

            return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Product value)
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
