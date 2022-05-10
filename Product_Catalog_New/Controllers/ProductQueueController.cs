using Microsoft.AspNetCore.Mvc;
using Product_Catalog_New.Models;
using Microsoft.EntityFrameworkCore;
using Product_Catalog_New.Services;
using System.Drawing;

namespace Product_Catalog_New.Controllers
{



    [Route("api/[controller]")]
    [ApiController]
    public class ProductQueueController : ControllerBase
    {
        private readonly ProductContext _context;
        private readonly IAppConfig _appconfig;
        private readonly ILogger<ProductController> _logger;
        private readonly IBlobService _blobService;

        private const string CONTAINER = "pictures";

        public ProductQueueController(ProductContext context, IAppConfig appConfig, ILogger<ProductController> logger, IBlobService blobService)
        {
            _context = context;
            _appconfig = appConfig;
            _logger = logger;
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
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

           

            // Create Bitmap object
            Bitmap bitmap = new Bitmap(1000, 800, System.Drawing.Imaging.PixelFormat.Format32bppPArgb);
            // Create and initialize Graphics
            Graphics graphics = Graphics.FromImage(bitmap);
            // Create Pen
            Pen pen = new Pen(Color.FromKnownColor(KnownColor.Blue), 2);
            // Draw arc
            graphics.DrawArc(pen, 0, 0, 700, 700, 0, 180);
            // Create another Pen
            Pen pen1 = new Pen(Color.FromKnownColor(KnownColor.Red), 2);
            // Draw ellipse
            graphics.DrawEllipse(pen1, 10, 10, 900, 700);

            MemoryStream memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
            memoryStream.Position = 0;

            string pictureName = product.Name + ".jpeg";

            await _blobService.UploadAsync(memoryStream, pictureName, "image/jpeg", CONTAINER);









            return CreatedAtAction("PostProduct", new { id = product.Id }, product);
        }


    }   
}