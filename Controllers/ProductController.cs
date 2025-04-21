using ExampeCrud.DTOs.Products;
using ExampeCrud.Services.Interfaces;
using ExampeCrud.Utils;
using ExampleCrud.Model;
using Microsoft.AspNetCore.Mvc;

namespace ExampleCrud.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController(IProductService productService) : ControllerBase
    {
        private readonly IProductService _productService = productService;

        [HttpGet("pagination")]
        public async Task<IActionResult> GetDataTable(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            try
            {
                var response = await _productService.GetDataTableAsync(page, pageSize, search);
                return Ok(response);
            }
            catch (Exception ex) 
            {
                return StatusCode(500, ApiResponseHelper.Failed<List<Product>>(ex.Message));
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(){
            try
            {
                var products = await _productService.GetProductAsync();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Failed<List<Product>>(ex.Message));
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var product = await _productService.GetProductByIdAsync(id);
                return Ok(product);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Failed<List<Product>>(ex.Message));
            }
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductRequestDto product)
        {
            try
            {
                var result = await _productService.SaveProduct(product);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponseHelper.Failed<List<Product>>(ex.Message));
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, ProductRequestDto update)
        {
            var product = await _productService.UpdateProduct(id,update);
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.DeleteProduct(id);
            return Ok(product);
        }
    }
}