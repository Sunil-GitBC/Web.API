using System.Collections.Generic;
using System.Threading.Tasks;
using Web.API.Dto;
using Web.API.Entity;

namespace Web.API.Services
{
    public interface IProductService
    {
        Task<IList<Product>> GetProducts(FilterDto filter);
        Task<Product> GetProductById(string id);
        Task AddProduct(Product product);
        Task DeleteProductById(string id);
        Task UpdateProductById(string id, Product product);
    }
}
