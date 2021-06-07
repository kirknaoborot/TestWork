using Microsoft.AspNetCore.Authorization;
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
    /// <summary>
    /// Контроллер работы с заказами
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        ApplicationContext _context;
        /// <summary>
        /// Контроллер работы с заказами
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public OrderController(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод получения списка заказов
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="dateStart">MM.dd.YYYY выбрать с</param>
        /// <param name="dateEnd">MM.dd.YYYY выбрать по</param>
        /// <returns>Список заказов </returns>
        [HttpGet("{clientId}/{dateStart}/{dateEnd}")]
        public async Task<IEnumerable<Order>> Get(Guid clientId, DateTime dateStart, DateTime dateEnd)
        {
            var orders = await _context.Orders.Where(x => x.Client.Id == clientId && x.CreateOrder >= dateStart && x.CreateOrder <= dateEnd).OrderBy(x => x.CreateOrder).Include(cl => cl.Client).ToListAsync();
            if (orders is null)
                throw new ArgumentNullException("clientId", "Не верно задан идентификатор клиента");
            return orders;
        }
        /// <summary>
        /// Метод добавления заказа
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="count">кол-во требуемого товара</param>
        /// <returns></returns>
        [HttpPost("{clientId}/{productId}/{count}")]
        public async Task<IActionResult> FromationOrder(Guid clientId, Guid productId, int count)
        {
            if (!BalanceProduct(productId, count))
                throw new Exception("Недостаточно товаров на складе");
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Id == productId);
            var client = await _context.Clients.FirstOrDefaultAsync(x => x.Id == clientId);

            if (product is null)
                throw new ArgumentNullException("productId","Не верно задан идентификатор продукта");
            if (client is null)
                throw new ArgumentNullException("clientId", "Не верно задан идентификатор клиента");

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
            SubtractProduct(product, count);
            _context.Products.Update(product);
            await _context.PositionOrders.AddAsync(positionOrder);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get),new {clientId = positionOrder.Order.Client.Id, dateStart = positionOrder.Order.CreateOrder, dateEnd = positionOrder.Order.CreateOrder},positionOrder.Order);
        }
        /// <summary>
        /// Метод определения кол-ва товара на складе
        /// </summary>
        /// <param name="productId">Идентификатор товара</param>
        /// <param name="count">кол-во требуемого товара</param>
        /// <returns></returns>
        private bool BalanceProduct(Guid productId, int count)
        {

            return _context.Products.First(x => x.Id == productId).CountProduct >= count;
        }
        /// <summary>
        /// Метод вычитания товара со склада
        /// </summary>
        /// <param name="product">Товар</param>
        /// <param name="count">Кол-во товара</param>
        private void SubtractProduct(Product product, int count)
        {
            product.CountProduct -= count;
            _context.Products.Update(product);
        }
    }
}
