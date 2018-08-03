using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Web.API.Dto;
using Web.API.Entity;

namespace Web.API.Services
{
    public class ProductService:IProductService
    {
        private readonly ProductDbContext _productDbContext;

        public ProductService(ProductDbContext productDbContext)
        {
            _productDbContext = productDbContext;
        }

        public async Task<IList<Product>> GetProducts(FilterDto filter)
        {
            if (filter == null) return await _productDbContext.Products.ToListAsync();

            return await _productDbContext.Products.Where(p =>
                    (filter.Brands != null && (filter.Brands.ToLowerInvariant().Split(',', StringSplitOptions.None).ToList().Contains(p.Brand.ToLowerInvariant())))||
                    (filter.Descriptions != null && (filter.Descriptions.ToLowerInvariant().Split(',', StringSplitOptions.None).ToList().Contains(p.Description.ToLowerInvariant())))||
                    (filter.Models != null && (filter.Models.ToLowerInvariant().Split(',', StringSplitOptions.None).ToList().Contains(p.Model.ToLowerInvariant()))))
                .ToListAsync();
        }
        
        public async Task<Product> GetProductById(string id)
        {
            return await _productDbContext.Products.FindAsync(id);
        }

        public async Task AddProduct(Product product)
        {
            if (product != null)
            {
                _productDbContext.Products.Add(product);
                await _productDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteProductById(string id)
        {
            var product = await _productDbContext.Products.FindAsync(id);
            if (product != null)
            {
                _productDbContext.Products.Remove(product);
                await _productDbContext.SaveChangesAsync();
            }
        }

        public async Task UpdateProductById(string id, Product product)
        {
            var prod = await _productDbContext.Products.FindAsync(id);
            if (prod != null)
            {
                prod.Brand = product.Brand;
                prod.Description = product.Description;
                prod.Model = product.Model;

                await _productDbContext.SaveChangesAsync();
            }
        }
    }
}
