using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;
using UnitOfWorkDemo.Core.Models;
using UnitOfWorkDemo.Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UnitOfWorkDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productDetailsList = await _productService.GetAllProducts();
            if (productDetailsList == null) return NotFound();
            return Ok(productDetailsList);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var productDetails = await _productService.GetProductById(productId);

            if (productDetails != null) return Ok(productDetails);
            else return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(ProductDetails productDetails)
        {
            var success = await _productService.CreateProduct(productDetails);

            if (success) return Ok(success);
            else return BadRequest();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProduct(ProductDetails productDetails)
        {
            if (productDetails != null)
            {
                var success = await _productService.UpdateProduct(productDetails);
                if (success) return Ok(success);                
            }
            return BadRequest();
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var success = await _productService.DeleteProduct(productId);

            if (success) return Ok(success);
            return BadRequest();
        }
    }
}
