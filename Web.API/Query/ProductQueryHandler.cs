using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Web.API.Entity;
using Web.API.Services;

namespace Web.API.Query
{
    public class ProductQueryHandler : 
        IMediatorQueryHandler<ProductsQuery, IList<Product>>,
        IMediatorQueryHandler<ProductQuery, Product>
    {
        private readonly IProductService _productService;

        public ProductQueryHandler(IProductService productservice)
        {
            _productService = productservice;
        }
        public async Task<IList<Product>> Handle(ProductsQuery query, CancellationToken cancellationToken)
        {
            return await _productService.GetProducts(query.Filter);
        }

        public async Task<Product> Handle(ProductQuery query, CancellationToken cancellationToken)
        {
            return await _productService.GetProductById(query.Id);
        }
    }
}
