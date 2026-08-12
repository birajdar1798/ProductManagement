
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;
using ProductManagementAPI.Services;

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

        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _context.Product.ToListAsync();
            return Ok(products);
        }

        [HttpGet("GetLatest")]
        public IActionResult GetLatest()
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
            var result = (
                from p in _context.Product
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
                }
            ).ToList();

            return Ok(result);
    
        }

        [HttpGet()]

        public IActionResult GetProduct( [FromQuery] int id)
        {
            var product = _context.Product.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                return NotFound("Product not found.");
            }
            else
            {
                return Ok(product);
            }
        }


        [HttpPost("AddProduct")]
        public IActionResult AddProduct([FromBody] Product products)
        {
            var product = _context.Product.FirstOrDefault(
                x => x.ProductName == products.ProductName
                );
            if (product == null)
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
            var product = _context.Product.FirstOrDefault(x => x.Id == id);
        
            var product1 = _context.Product.FirstOrDefault(x => x.ProductName == products.ProductName && x.Price == products.Price && x.IsActive == products.IsActive);

            if (product1 == null)
            {
                if (id == 0)
                {
                    products.CreatedBy = "Created_user";
                    products.CreatedOn = DateTime.Now;
                    products.ModifiedOn = null;
                    _context.Product.Add(products);
                    _context.SaveChanges();
                    return Ok("Product successfully Created");

                } else
                {
                    product.Id = products.Id;
                    product.ProductName = products.ProductName;
                    product.Price = products.Price;
                    product.CreatedBy = products.CreatedBy;
                    product.CreatedOn = product.CreatedOn;
                    product.ModifiedBy = "Updated_user";
                    product.ModifiedOn = DateTime.Now;
                    product.IsActive = products.IsActive;
                    
                        _context.SaveChanges();
                        return Ok("Product successfully Updated");
                    

                }
                
                
                
            }
            else {
                return Ok("Product Already Added");
            }
            

}

        [HttpDelete("{ids}")]

        public IActionResult DeleteProduct(string ids)
        {
            bool isExistItemid = false;
            List<int> Listids = ids.Split(',', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();
            
            for (int i = 0; i < Listids.Count; i++)
            {
                bool exists = _context.Item.Any(x => x.ProductId == Listids[i]);
                if (exists)
                {
                    isExistItemid = true;
                
                } else
                {
                    var product = _context.Product.FirstOrDefault(x => x.Id == Listids[i]);
                    _context.Product.Remove(product);
                    _context.SaveChanges();

                }
                
            }
            if (isExistItemid)
            {
                return Ok(false);
            }
            else 
            {
                return Ok(true);

            }
        }

        [HttpGet("test")]
        public IActionResult GetProduct()
        {
            GetProducts getproducts = new GetProducts(_context);
            var products = getproducts.GetProductss;
            return Ok(products);
        }
    }
}
