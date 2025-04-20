using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful_API.Data;
using RESTful_API.Model;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController(AppDbContext dp)
        {
            _dp = dp;
        }
        private AppDbContext _dp;

        [HttpGet]
        public async Task<IActionResult> GetCategories()
        {
            var cats = await _dp.Categories.ToListAsync();
            return Ok(cats);
        }

        [HttpPost]
        public async Task<IActionResult> AddCategory(string Cat)
        {
            Category c = new() { Name = Cat };
            await _dp.Categories.AddAsync(c);
            _dp.SaveChanges();
            return Ok(c.Id);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory(Category Cat) /// [FromBody] or Category Cat
        {
            var c = await _dp.Categories.SingleOrDefaultAsync(c => c.Id == Cat.Id);
            if (c == null) { return NotFound($" this id {Cat.Id} not exists !"); }
            c.Name=Cat.Name;
            _dp.SaveChanges();
            return Ok("Category Updated!");
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> RemoveCategory(int id)
        {
            var c = await _dp.Categories.SingleOrDefaultAsync(c=>c.Id == id);
            if (c == null) { return NotFound($" this id {id} not exists !"); }
            _dp.Remove(c);
            _dp.SaveChanges();
            return Ok(c);
        }
    }
}
