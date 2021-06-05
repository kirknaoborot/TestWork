using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWork.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ApplicationContext _context;

        public OrderController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("Orders")]
        public IEnumerable<Order> GetOrders([FromBody] Client client, DateTime dateStart, DateTime dateEnd)
        {
            var orders = _context.Orders.Where(x => x.Client == client && x.CreateOrder >= dateStart && x.CreateOrder <= dateEnd).OrderBy(x => x.CreateOrder);
            return orders;
        }

        //[HttpPost("FormationOrder")]
        //public async Task<IActionResult> PostFromationOrder([FromBody]Client client, [FromBody]Product product, int count)
        //{
        //    PositionOrder positionOrder = new PositionOrder()
        //    {
        //        Count = count,
        //        Price = product.PriceProduct * count,
        //        Product = product,
        //        Order = new Order { Client = client, CreateOrder = DateTime.Now.Date }
        //    };
        //    product.CountProduct = product.CountProduct - count;
        //    await _context.Orders.AddAsync(positionOrder.Order);
        //    await _context.PositionOrders.AddAsync(positionOrder);
        //    _context.Products.Update(product);
        //    await _context.SaveChangesAsync();
        //    return CreatedAtAction(nameof(GetProductTypes),client);
        //}
    }
}
