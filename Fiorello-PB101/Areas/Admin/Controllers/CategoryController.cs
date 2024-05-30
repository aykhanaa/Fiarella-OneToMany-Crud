using Fiorello_PB101.Data;
using Fiorello_PB101.Models;
using Fiorello_PB101.Services.Interfaces;
using Fiorello_PB101.ViewModels.Categories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorello_PB101.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly AppDbContext _context;
        public CategoryController(ICategoryService categoryService,AppDbContext context)
        {
            _categoryService = categoryService;
            _context=context;
        }
        [HttpGet]
        public async Task< IActionResult> Index()
        { 
            return View(await _categoryService.GetAllWithProductCountAsync());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>Create(Category category)
        {

            if (!ModelState.IsValid)
            {
                return View();
            }
            bool existCategory = await _categoryService.ExistAsync(category.Name); 
            if (existCategory)
            {   
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }
            await _categoryService.CreateAsync(new Category { Name = category.Name }); 
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();
            var catgory =  await _categoryService.GetByIdAsync((int)id);
            if(catgory is null) return NotFound();
            await _categoryService.DeleteAsync(catgory);

            return RedirectToAction(nameof(Index));
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id is null) return BadRequest();
            var catgory = await _categoryService.GetByIdAsync((int)id);
            if (catgory is null) return NotFound();
            return View(new CategoryEditVM { Name = catgory.Name});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, CategoryEditVM request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (id is null) return BadRequest();

            if(await _categoryService.ExistExceptByIdAsync((int)id,request.Name))
            {
                ModelState.AddModelError("Name", "This category already exist");
                return View();
            }

            var catgory = await _categoryService.GetByIdAsync((int)id);

            if (catgory is null) return NotFound();

            if (catgory.Name == request.Name)
            {
                return RedirectToAction(nameof(Index));
            }

            catgory.Name = request.Name;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> SetToArchive(int? id)
        {
            if (id is null) return BadRequest();
            var catgory = await _categoryService.GetByIdAsync((int)id);
            if (catgory is null) return NotFound();
            catgory.SoftDeleted  = true;
            await _context.SaveChangesAsync();
            return Ok(catgory);
        }

    }
}
