using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWork.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestWork.Controllers
{
    /// <summary>
    /// Контроллер работы типа товаров
    /// </summary>
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductTypeController : ControllerBase
    {
        ApplicationContext _context;
        /// <summary>
        /// Контроллер работы типа товаров
        /// </summary>
        public ProductTypeController(ApplicationContext context)
        {
            _context = context;
        }
        /// <summary>
        /// Метод получения типов товара
        /// </summary>
        /// <returns></returns>
        // GET: api/<ProductTypeController>
        [HttpGet]
        [ResponseCache(Location = ResponseCacheLocation.Any, Duration = 100)]
        public async Task<IEnumerable<ProductType>> Get()
        {            
           var productType = await _context.ProductTypes.ToListAsync();
           return productType;
        }
        /// <summary>
        /// Метод добавления типа товара
        /// </summary>
        /// <param name="productType">Имя продукта</param>
        ///// <response code = "201" > Тип продукта</response>
        ///// <response code = "400" > Тип продукта был Null</response>
        ///// <response code = "500" > Ошибка сервера</response>
            [HttpPost]
        //[ProducesResponseType(typeof(ProductType), 200)]
        //[ProducesResponseType(typeof(ProductType), 400)]
        //[ProducesResponseType(500)]
        public async Task<IActionResult> Post([FromBody] ProductType productType)
        {
            if (productType is null || String.IsNullOrEmpty(productType.NameType))
                throw new ArgumentNullException(nameof(productType));
            await _context.ProductTypes.AddAsync(productType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), productType);
        }
    }
}
