using System.Reflection.Metadata.Ecma335;
using Microsoft.EntityFrameworkCore;
using ProductManagementAPI.Data;
using ProductManagementAPI.Dto;

namespace ProductManagementAPI.Services
{
    
    public class GetProducts
    {
        private readonly ApplicationDbContext _context;

        public GetProducts(ApplicationDbContext context)
        {
            _context = context ;
        }
        public List<Product> GetProductss() 
        {
            
            var products = _context.Product.ToList();
            return products;
        }






    }
}
