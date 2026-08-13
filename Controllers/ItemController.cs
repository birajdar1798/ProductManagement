using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ItemController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet("GetAllItems")]



        //https 7356 /api/Product/GetAllProducts

        public IActionResult GetAllItems()
        {
            var items = _context.Item.ToList();

            if (items.Count > 0)
            {
                return Ok(items);

            }
            else
            {
                return Ok("No data Available");
            }
        }

        [HttpPost("AddItems")]

        public IActionResult AddItems([FromBody] Item item)
        {

            var item1 = _context.Item.FirstOrDefault(i => i.Name == item.Name);
            if (item1 == null)
            {

                _context.Item.Add(item);
                _context.SaveChanges();
                return Ok(true);
            }
            else
            {
                return Ok(false);
            }

        }

        [HttpDelete]

        public IActionResult DeleteItem([FromQuery] int id)
        {

            var item = _context.Item.FirstOrDefault(item => item.Id == id);

            if (item == null)
            {
                return NotFound("Item with id do not exists.");
            }
            else
            {
                _context.Item.Remove(item);
                _context.SaveChanges();
                return Ok("Item Deleted Successfully");
            }
        }

        [HttpGet("getItemById")]

        public IActionResult getItemById([FromQuery] int id)
        {
            var item = _context.Item.FirstOrDefault(item => item.Id == id);
            if(item == null)
            {
                return BadRequest("Item does not exists for id");
            }
            else
            {
                return Ok(item);
            }

        }

    }
}