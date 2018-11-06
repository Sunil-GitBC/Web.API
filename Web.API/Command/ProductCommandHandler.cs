using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Web.API.Services;

namespace Web.API.Command
{
    public class ProductCommandHandler : IMediatorCommandHandler<ProductCommand>
    {
        private readonly IProductService _productService;

        public ProductCommandHandler(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<Unit> Handle(ProductCommand command, CancellationToken cancellationToken)
        {
            switch (command.ProductOperation)
            {
                case ProductOperation.ADD:
                    await _productService.AddProduct(command.Product);
                    break;
                case ProductOperation.DELETE:
                    await _productService.DeleteProductById(command.Id);
                    break;
                case ProductOperation.UPDATE:
                    await _productService.UpdateProductById(command.Id, command.Product);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return Unit.Value;
        }
    }
}
