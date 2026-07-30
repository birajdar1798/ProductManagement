
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;

namespace ProductManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class ProductController : ControllerBase 
    {

        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllProducts")]

        //https 7356 /api/Product/GetAllProducts

        public IActionResult GetAllProducts()
        {
            var products = _context.Product.ToList();

            if (products.Count > 0)
            {
                return Ok(products);

            }
            else
            {
                return Ok("No data Available");
            }
        }

        [HttpGet("GetProductsItem")]
        [Authorize]


        public IActionResult GetAllProductsItem() 
        { 
            var Result = (from p in _context.Product
                          join I in _context.Item on p.Id equals I.ProductId
                          select new ProductItem
                          {
                                Id = p.Id,
                                ProductName = p.ProductName,
                                CreatedBy = p.CreatedBy,
                                CreatedOn = p.CreatedOn,
                                ModifiedBy = p.ModifiedBy,
                                ModifiedOn = p.ModifiedOn,
                                Quantity = I.Quantity
                          }).ToList();

            return Ok(Result);
    
        }

        [HttpGet("{id}")]

        public IActionResult GetProduct(int id)
        {
            var Product = _context.Product.FirstOrDefault(x => x.Id == id);
            if (Product == null)
            {
                return NotFound("Product not found.");
            }
            else
            {
                return Ok(Product);
            }
        }


        [HttpPost("AddProduct")]
        public IActionResult AddProdut([FromBody] Product products)
        {
            //var Product = _context.Product.ToList();
            var Product = _context.Product.FirstOrDefault(x => x.ProductName == products.ProductName);
            if (Product == null)
            {
                products.CreatedOn = DateTime.Now;
                products.ModifiedOn = null;
                _context.Product.Add(products);
                _context.SaveChanges();
                return Ok(true);

            }
            else
            {
                return Ok(false);
            }
        }

    

        [HttpPut("{id}")]

        public IActionResult UpdateProduct(int id, [FromBody] Product products)
        {
            var Product = _context.Product.FirstOrDefault(x => x.Id == id);
            if (Product == null)
            {
                return NotFound("Product not found.");
            }
            else
            {
                Product.Id = id;
                Product.ProductName = products.ProductName;
                Product.CreatedBy = products.CreatedBy;
                Product.CreatedOn = Product.CreatedOn;
                Product.ModifiedBy = products.ModifiedBy;
                Product.ModifiedOn = DateTime.Now;


                _context.SaveChanges();
                return Ok(Product);
            }

        }

        [HttpDelete]

        public IActionResult DeleteProduct([FromQuery]int id)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == id);

            bool exists = _context.Item.Any(x => x.ProductId == id);

            if (exists)
            {
                return BadRequest("Cannot delete this product because it is used by ProductItems.");
            }
            else
            {
                if (product == null)
                {
                    return NotFound("Product not found.");
                }
                else
                {
                    _context.Product.Remove(product);
                    _context.SaveChanges();
                    return Ok("Product deleted successfully.");
                }

            }
        }
    }
    }
