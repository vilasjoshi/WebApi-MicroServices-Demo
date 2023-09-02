using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProductService.Data;
using ProductService.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProductService.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        [HttpGet("id")]
        public Product? Get(int id)
        {
            return _context.Products.Where(p => id == p.Id).FirstOrDefault();
        }
    }
}

