using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Infrastructure;
using TestWork.Models;


namespace TestWork.Controllers
{
    /// <summary>
    /// Контроллер работы с товаром
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        ApplicationContext _context;
        /// <summary>
        /// Контроллер работы с товаром
        /// </summary>
        public ProductController(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод получения списка товаров
        /// </summary>
        /// <param name="productType">Тип продукта</param>
        /// <param name="availableProduct">Наличие товара на складе</param>
        /// <param name="sortPrice">Сортировать по цене</param>
        /// <returns>Список товаров</returns>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100)]
        public async Task<IEnumerable<Product>> Get(Guid? productType = null, bool? availableProduct = null, bool? sortPrice = null)
        {
            var products = _context.Products.Include(pt => pt.ProductType).AsQueryable();
            if (productType != null && productType != Guid.Empty)
                products = products.Where(x => x.ProductType.Id == productType);
            if ((availableProduct == true))
                products = products.Where(x => x.CountProduct > 0);
            if (sortPrice == true)
                products = products.OrderBy(x => x.PriceProduct);
            return await products.ToListAsync();
        }
        /// <summary>
        /// Метод добавления товаров
        /// </summary>
        /// <param name="addProduct"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddProduct addProduct)
        {
            if (addProduct is null)
            {
                throw new ArgumentNullException(nameof(addProduct));
            }
            var productType = await _context.ProductTypes.FirstOrDefaultAsync(x => x.Id == addProduct.ProductTypeId);
            if (productType is null)
            {
                throw new ArgumentNullException("ProductTypeId","Не верно указан идентификатор типа продукта");
            }
            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                NameProduct = addProduct.NameProduct,
                CountProduct = addProduct.CountProduct,
                PriceProduct = addProduct.PriceProduct,
                ProductType = productType
            };
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), product);
        }
    }
}
