using APIEcommerce.EC;
using ecommercelibrary.DTO;
using ecommercelibrary.models;
using ecommercelibrary.Utilities;
using ecommercelibrary.Utility;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using springecommerce.models;

namespace APIEcommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Item?> Get()
        {
            return new InventoryEC().Get();

        }
        [HttpGet("{id}")]
        public Item? GetById(int id)
        {
            var result = new WebRequestHandler().Get($"/Inventory/{id}").Result;
            return JsonConvert.DeserializeObject<Item>(result);
        }

        [HttpDelete("Delete/{id}")]
        public Item? Delete(int id)
        {
            return new InventoryEC().Delete(id);
        }

        [HttpPost]
        public Item? AddOrUpdate([FromBody]Item item)
        {
            
            var newItem = new InventoryEC().AddOrUpdate(item); 
            return item;
        }
        [HttpPost("Search")]
        public IEnumerable<Item> Search([FromBody] QueryRequest query)
        {
            return new InventoryEC().Get(query.Query    ); 
        }
    }
}
