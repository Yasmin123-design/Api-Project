using E_Commerce.Migrations;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartController : ControllerBase
    {
        private readonly IShoppingCartItems _items;
        IFoodRepository foodRepository;
        public ShoppingCartController(IShoppingCartItems items , IFoodRepository _foodRepository)
        {
            _items = items;
            foodRepository = _foodRepository;
        }
        [HttpPost("AddToCart/{id}")]
        public IActionResult AddToCart([FromRoute]int id)
        {
            var item = foodRepository.GetById(id);
            if(item == null)
            {
                return BadRequest("no food provided");
            }
            else
            {
                _items.AddToCart(item);
                List<ShoppingCartItems> items = _items.All();
                return Ok(new { message = "Items added to cart successfully.", cartItems = items });
            }
                 
        }
        [HttpPost("RemoveFromCart{id}")]
        public IActionResult RemoveFromCart([FromRoute]int id)
        {
            var item = foodRepository.GetById(id);
            if (item == null)
            {
                return BadRequest("no food provided");
            }
            else
            {
                _items.RemoveFromCart(item);
                List<ShoppingCartItems> items = _items.All();
                return Ok(new { message = "Items removed from cart successfully.", cartItems = items });
            }
        }
        [HttpPost("clear")]
        public IActionResult Clear()
        {
            _items.Clear();
            List<ShoppingCartItems> items = _items.All();
            return Ok(new { message = "cart cleared successfully.", cartItems = items });
        }
    }
}
