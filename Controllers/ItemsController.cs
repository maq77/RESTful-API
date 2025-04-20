using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RESTful_API.Data;
using RESTful_API.Model;
using static Azure.Core.HttpHeader;
using static System.Net.Mime.MediaTypeNames;
using System.IO;

namespace RESTful_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        public ItemsController(AppDbContext dp)
        {
            _dp = dp;
        }
        private readonly AppDbContext _dp;
        [HttpGet]
        public async Task<IActionResult> GetItems() {
            var items = await _dp.Items.ToListAsync();
            return Ok(items);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetItem(int id)
        {
            var item = await _dp.Items.FirstOrDefaultAsync(c=>c.Id==id);
            if(item == null) { return NotFound($" Item id {id} not exists!"); }
            return Ok(item);
        }
        [HttpGet("Cat/{CategoryID}")]
        public async Task<IActionResult> GetItemsOfCategoryID(int CategoryID)
        {
            var items = await _dp.Items.Where(x => x.CategoryId == CategoryID).ToListAsync();
            if (items==null) { return NotFound($"Category ID : {CategoryID} Doesn't Have Items !"); }
            return Ok(items);
        }
        [HttpPost] 
        public async Task<IActionResult> AddItem([FromForm]mdlItem mdl) // making it as Form
        {
            var item = new Item
            {
                Name = mdl.Name,
                Price = mdl.Price,
                Notes = mdl.Notes,
                CategoryId = mdl.CategoryId
            };
            if (mdl.Image != null)
            {
                using var stream = new MemoryStream(); ///upload images
                await mdl.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }
            await _dp.Items.AddAsync(item);
            await _dp.SaveChangesAsync();
            return Ok(item);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateItem(int id, [FromForm] mdlItem mdl)
        {
            var item = await _dp.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) { return NotFound($"item : {id} not found!"); }
            if (mdl.Image != null) { 
                using var stream = new MemoryStream();
                await mdl.Image.CopyToAsync(stream);
                item.Image = stream.ToArray();
            }
            item.Name = mdl.Name;
            item.Notes = mdl.Notes;
            item.CategoryId = mdl.CategoryId;
            await _dp.SaveChangesAsync();
            return Ok(item);

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteByID(int id)
        {
            var item = await _dp.Items.FirstOrDefaultAsync(x => x.Id == id);
            if (item == null) { return NotFound($"id: {id} not found!"); }
            _dp.Items.Remove(item);
            await _dp.SaveChangesAsync();
            return Ok(item + " Deleted Successfully !");
        }
    }
}
