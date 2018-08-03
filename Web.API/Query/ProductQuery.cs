using Web.API.Entity;

namespace Web.API.Query
{
    public class ProductQuery : IMediatorQuery<Product>
    {
        public string Id { get; set; }

        public ProductQuery(string id)
        {
            Id = id;
        }
    }
}
