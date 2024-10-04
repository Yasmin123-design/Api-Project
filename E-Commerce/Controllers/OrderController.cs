using E_Commerce.Dto;
using E_Commerce.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IShoppingCartItems cartitems;
        IOrderRepository orderRepository;
        public OrderController(IShoppingCartItems _cartitems , IOrderRepository order)
        {
            orderRepository = order;
            cartitems = _cartitems;
        }

        [HttpPost("CreateOrder")]
        public IActionResult CreateOrder([FromBody]Order order)
        {
            var items = cartitems.All();
            if(items.Count() == 0)
            {
                return BadRequest("shopping cart is empty");
            }
            orderRepository.CreateOrder(order);
            int count = order.Items.Count();
            cartitems.Clear();
            return Ok(new { message = "Order created successfully.",  NoOfItems = count});
            
        }
    }
}
