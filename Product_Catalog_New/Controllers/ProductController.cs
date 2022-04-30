using Microsoft.AspNetCore.Mvc;
using Product_Catalog_New.Models;
using Microsoft.EntityFrameworkCore;


namespace Product_Catalog_New.Controllers
{



    [Route("api/products")]
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


        // Neues Produkt hinzufügen 
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(Product product)
        {
            _logger.LogInformation("Produkt wurde  erstellt: " + product.Id); 
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            // Fügt Suffix aus Appsettingsfile hinzu 
            product.Description = product.Id + _appconfig.GetSuffix();
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostProduct", new { id = product.Id }, product);
        }

        // Ändern eine Produktes 
        [HttpPut("{id}")]
        public async Task<ActionResult> PutProduct(long id, Product product)
        {
            _logger.LogInformation("Produkt mit der ID: " + product.Id + "wurde geändert"); 
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

        // Löscht das Element mit der mitgeschickten Id  
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct(long id)
        {
            _logger.LogInformation("Produkt wurde gelöscht id: " + id ); 
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Liefert alle vorhanden Produkte zurück 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            return await _context.Products.ToListAsync();
            _logger.LogInformation("Get the Products."); 
        }







    }
}