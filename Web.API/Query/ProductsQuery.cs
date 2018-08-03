using System.Collections.Generic;
using Web.API.Dto;
using Web.API.Entity;

namespace Web.API.Query
{
    public class ProductsQuery : IMediatorQuery<IList<Product>>
    {
        public FilterDto Filter { get; set; }
    }
}
