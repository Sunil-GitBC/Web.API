using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.API.Dto;
using Web.API.Entity;
using Web.API.Query;

namespace Web.API.Controllers
{

    [Route("api/[controller]")]
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IList<Product>> Get([FromQuery] FilterDto filterOptions)
        {
            var query = new ProductsQuery();
            if (filterOptions.Brands != null || filterOptions.Descriptions != null || filterOptions.Models != null)
            {
                query.Filter = filterOptions;
            }

            return await _mediator.Send(query);
        }

        [HttpGet("{id}")]
        public async  Task<Product> Get(string id)
        {
            return await _mediator.Send(new ProductQuery(id));
        }

        [HttpPost]
        [Authorize("write:products")]
        public async Task Post([FromBody]Product product)
        {
            await _mediator.Send(new Command.ProductCommand(ProductOperation.ADD, product));
        }

        [HttpPut("{id}")]
        [Authorize("write:products")]
        public async Task Put(string id, [FromBody]Product product)
        {
            await _mediator.Send(new Command.ProductCommand(ProductOperation.UPDATE, id, product));
        }

        [HttpDelete("{id}")]
        [Authorize("write:products")]
        public async Task Delete(string id)
        {
            await _mediator.Send(new Command.ProductCommand(ProductOperation.DELETE, id));
        }
    }
}
