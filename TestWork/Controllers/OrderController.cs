using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet("{clientId}/{dateStart}/{dateEnd}")]

        public async Task<IEnumerable<Order>> GetOrders(Guid clientId, DateTime dateStart, DateTime dateEnd)
        {
            var orders = await _context.Orders.Where(x => x.Client.Id == clientId && x.CreateOrder >= dateStart && x.CreateOrder <= dateEnd).OrderBy(x => x.CreateOrder).Include(cl=>cl.Client).ToListAsync();
            return orders;
        }

        [HttpPost("{clientId}/{productId}/{count}")]
        public async Task<IActionResult> PostFromationOrder(Guid clientId, Guid productId, int count)
        {
            if (!BalanceProduct(productId).Result)
                return BadRequest();
            var product = await _context.Products.FirstAsync(x => x.Id == productId);
            var client = await _context.Clients.FirstAsync(x => x.Id == clientId);

            PositionOrder positionOrder = new PositionOrder()
            {
                Id = Guid.NewGuid(),
                Count = count,
                Price = product.PriceProduct * count,
                Product = product,
                Order = new Order
                {
                    Id = Guid.NewGuid(),
                    Client = client,
                    CreateOrder = DateTime.Now.Date
                }
            };
            product.CountProduct -= count;
            _context.Products.Update(product);
            await _context.PositionOrders.AddAsync(positionOrder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetOrders), positionOrder);
        }

        private async Task<bool> BalanceProduct(Guid productId)
        {
            return await _context.Products.AnyAsync(x => x.Id == productId);
        }
    }
}
