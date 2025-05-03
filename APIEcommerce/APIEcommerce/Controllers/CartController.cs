using Microsoft.AspNetCore.Mvc;
using ecommercelibrary.models;
using APIEcommerce.EC;
using ecommercelibrary.Utility;

namespace APIEcommerce.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Item> Get() => new CartEC().Get();

        [HttpPost]
        public Item AddOrUpdate([FromBody] Item item) => new CartEC().AddOrUpdate(item);

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            bool removed = new CartEC().Remove(id);
            return removed ? Ok() : NotFound();
        }

        [HttpGet("Total")]
        public decimal GetTotal() => new CartEC().GetTotal();

        [HttpPost("Checkout")]
        public IActionResult Checkout()
        {
            new CartEC().Checkout();
            return Ok();
        }

        [HttpPost("Search")]
        public IEnumerable<Item> Search([FromBody] QueryRequest query)
        {
            return new CartEC().Search(query.Query);
        }
    }
}
