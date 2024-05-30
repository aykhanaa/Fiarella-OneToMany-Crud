using Fiorello_PB101.Data;
using Fiorello_PB101.ViewModels.Sliders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderController:Controller
    {
        private readonly AppDbContext _context;

        public SliderController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<SliderVM> sliders = await _context.Sliders.Select(m=>new SliderVM { Id = m.Id, Image = m.Image})
                                                           .ToListAsync();
            return View(sliders);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderCreateVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            foreach(var item in request.Images)
            {
                if (!item.ContentType.Contains("image/"))
                {
                    ModelState.AddModelError("Image", "Input can accept only image format");
                    return View();
                }

                if (!(item.Length / 1024 < 200))
                {
                    ModelState.AddModelError("Image", "Image size must be max 200 KB");
                    return View();
                }

            }
          
            return RedirectToAction(nameof(Index));
        }
    }
}
