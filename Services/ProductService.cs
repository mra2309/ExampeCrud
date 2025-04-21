using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampeCrud.DTOs.Products;
using ExampeCrud.Services.Interfaces;
using ExampeCrud.Utils;
using ExampleCrud.Data;
using ExampleCrud.Model;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace ExampeCrud.Services
{
    public class ProductService : IProductService
    {
        private readonly AppDbContext _context;

        public ProductService(AppDbContext context,IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
        }

        public async Task<object> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null) return ApiResponseHelper.Failed<List<Product>>("Product Not Found");

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return ApiResponseHelper.Success("Success Delete Data");
        }

        public async Task<object> GetProductAsync()
        {
            var products = await _context.Products.ToListAsync();
            return ApiResponseHelper.Success(products);
        }

        public async Task<object> GetProductByIdAsync(int id)
        {
            var products = await _context.Products.FindAsync(id);
            return ApiResponseHelper.Success(products);
        }
        public async Task<PageResponse<Product>> GetDataTableAsync(int page, int pageSize, string? search)
            {
                var query = _context.Products.AsQueryable();

                if (!string.IsNullOrEmpty(search))
                {
                    query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
                }

                var totalItems = await query.CountAsync();
                var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

                var products = await query
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                return new PageResponse<Product>
                {
                    Status = "success",
                    Data = (IEnumerable<Product>)products,
                    Meta = new Meta
                    {
                        Page = page,
                        PageSize = pageSize,
                        TotalItem = totalItems,
                        TotalPages = totalPages
                    }
                };
        }

        public async Task<object> SaveProduct(ProductRequestDto productDto)
        {
            var product = productDto.ToEntity();
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return ApiResponseHelper.Success(product);
        }

        public async Task<object> UpdateProduct(int id,ProductRequestDto update)
        {
            var product = await _context.Products.FindAsync(id);
            if(product == null) return ApiResponseHelper.Failed<string>("Product Not Found");

            product.Name = update.Name;
            product.Price = update.Price;
            await _context.SaveChangesAsync();
            // return NoContent();
            return ApiResponseHelper.Success<string>("success update product!");
        }

        public Task<object> SaveProduct()
        {
            throw new NotImplementedException();
        }
    }
}