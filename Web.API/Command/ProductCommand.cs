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
        }
        public ProductCommand(ProductOperation operation, Product product)
        {
        }
        public ProductCommand(ProductOperation operation, string id, Product product)
        {
        }
    }
}
