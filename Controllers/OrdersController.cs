using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RESTful_API.Data;
using RESTful_API.Model;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        public OrdersController(AppDbContext dp)
        {
            _dp = dp;
        }
        private readonly AppDbContext _dp;
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = _dp.Orders.ToList();
            return Ok(orders);
        }
        [HttpPost]
        public async Task<IActionResult> GetAllOrders(Order order)
        {
            //await _dp.Orders.AddAsync(order);
            //await _dp.SaveChangesAsync();
            return Ok(order);
        }
    }
}
