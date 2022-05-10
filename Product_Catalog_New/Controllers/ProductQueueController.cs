using Microsoft.AspNetCore.Mvc;
using Product_Catalog_New.Models;
using Microsoft.EntityFrameworkCore;
using Product_Catalog_New.Services;

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

        public ProductQueueController(ProductContext context, IAppConfig appConfig, ILogger<ProductController> logger, IBlobService blobService)
        {
            _context = context;
            _appconfig = appConfig;
            _logger = logger;
            _blobService = blobService ?? throw new ArgumentNullException(nameof(blobService));
        }


    }   
}