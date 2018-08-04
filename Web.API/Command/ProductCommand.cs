using System.Runtime.InteropServices.ComTypes;
using Web.API.Entity;

namespace Web.API.Command
{
    public class ProductCommand : IMediatorCommand
    {
        public ProductOperation ProductOperation { get; set; }
        public string Id { get; set; }

        public Product Product { get; set; }

        public ProductCommand(ProductOperation operation, string id)
        {
            ProductOperation = operation;
            Id = id;
        }
        public ProductCommand(ProductOperation operation, Product product)
        {
            ProductOperation = operation;
            Product = product;
        }
        public ProductCommand(ProductOperation operation, string id, Product product)
        {
            ProductOperation = operation;
            Id = id;
            Product = product;
        }
    }
}
