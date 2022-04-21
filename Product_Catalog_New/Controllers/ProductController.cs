using Microsoft.AspNetCore.Mvc;
using Product_Catalog_New.Models;
using Microsoft.EntityFrameworkCore;


namespace Product_Catalog_New.Controllers
{



    [Route("api/[products]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IAppConfig _appconfig;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductContext context, IAppConfig appConfig, ILogger<ProductController> logger)
        {
            _context = context;
            _appconfig = appConfig;
            _logger = logger;
        }



        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _logger.LogInformation("Create Product with id: " + product.Id); 
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            product.Description = product.Id + _appconfig.GetSuffix();
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostProduct", new { id = product.Id }, product);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(long id, Product product)
        {
            _logger.LogInformation("Change Product with´the id: " + product.Id); 
            if (id != product.Id)
            {
                return BadRequest();
            }
            _context.Entry(product).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Products.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();

        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            _logger.LogInformation("Delete Product with id: " + id ); 
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
            _logger.LogInformation("Get the Products."); 
        }







    }
}