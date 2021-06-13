using Microsoft.AspNetCore.Mvc;
using ShopBridge.Data.Repository;
using ShopBridge.Models;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ShopBridge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet("ReadProduct")]
        public async Task<Product> ReadProduct([FromQuery] int Id)
        {
            var result = await _productRepository.ReadProduct(Id);
            return result;
        }

        [HttpPost("UpsertProduct")]
        public async Task<int> UpsertProduct([FromBody] Product product)
        {
            var result = await _productRepository.Upsert(product);
            return result;
        }

        [HttpPost("DeleteProduct")]
        public async Task<int> DeleteProduct([FromQuery] int Id)
        {
            var result = await _productRepository.DeleteProduct(Id);
            return result;
        }

        [HttpGet("ReadProducts")]
        public async Task<List<Product>> ReadProducts()
        {
            return await _productRepository.ReadProducts();
        }

        [HttpPost("AddProduct")]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            var result = await _productRepository.Upsert(product);
            return Ok(result);
        }
    }
}
