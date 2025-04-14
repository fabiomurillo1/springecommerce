using APIEcommerce.EC;
using ecommercelibrary.DTO;
using ecommercelibrary.models;
using Microsoft.AspNetCore.Mvc;
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
            //return new List<Product>
            //{
            //    new Product{ Id = 1, Name = "Something 1"},
            //    new Product{ Id = 2, Name = "Something 2"},
            //    new Product{ Id = 3, Name = "Something 3"}
            //};

        }
        [HttpGet("/{id}")]
        public Item? GetById(int id)
        {
            return new InventoryEC().Get().FirstOrDefault(i => i.Id == id);
        }
    }
}
