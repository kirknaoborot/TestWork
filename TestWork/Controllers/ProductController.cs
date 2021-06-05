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
    public class ProductController : ControllerBase
    {
        ApplicationContext _context;

        public ProductController(ApplicationContext context)
        {
            _context = context;
        }

        [HttpGet("ProductsType")]
        public IEnumerable<ProductType> GetProductTypes()
        {
            var productType = _context.ProductTypes;
            return productType;
        }

        [HttpGet("Products")]
        public IEnumerable<Product> GetProducts(Guid? productType = null, bool? availableProduct = null, bool? sortPrice = null)
        {
            IQueryable<Product> products = _context.Products.Include(pt => pt.ProductType);
            if (productType != null && productType != Guid.Empty)
                products.Where(x => x.ProductType.Id == productType);
            if ((availableProduct == true) && (availableProduct != null))
                products.OrderBy(x => x.CountProduct);
            if (sortPrice == true && sortPrice != null)
                products.OrderBy(x => x.PriceProduct);
            return products;
        }

        [HttpPost("Product")]
        public async Task<IActionResult> PostProduct([FromBody] Product product)
        {
            if (product is null)
            {
                throw new ArgumentNullException(nameof(product));
            }
            product.ProductType = _context.ProductTypes.First(x => x.NameType == product.ProductType.NameType);
            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProducts), product);
        }
        // POST api/<HomeController>/product
        [HttpPost("ProductType")]
        public async Task<IActionResult> PostProductType([FromBody] ProductType productType)
        {
            await _context.ProductTypes.AddAsync(productType);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetProductTypes), productType);
        }

    }
}
