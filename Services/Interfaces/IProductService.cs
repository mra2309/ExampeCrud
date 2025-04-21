using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExampeCrud.DTOs.Products;
using ExampeCrud.DTOs.Products;
using ExampleCrud.Model;

namespace ExampeCrud.Services.Interfaces
{
    public interface IProductService
    {
        Task<PageResponse<Product>> GetDataTableAsync(int page, int pageSize, string? search);
        Task<object> GetProductAsync();
        Task<object> GetProductByIdAsync(int id);
        Task<object> SaveProduct(ProductRequestDto pro);
        Task<object> UpdateProduct(int id,ProductRequestDto update);
        Task<object> DeleteProduct(int id);
    }
}