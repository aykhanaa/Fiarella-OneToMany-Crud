using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fiorello_PB101.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            
            _productService = productService;
        }
        public async Task< IActionResult> Index(int? id)
        {
            if (id == null) return BadRequest();
            Product product = await _productService.GetByIdWithAllDatasAsync((int)id);
            if (product == null) return NotFound();
            return View(product);
        }
    }
}
