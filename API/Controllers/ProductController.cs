using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
using Application.Features.Products.Dtos;
using Application.Features.Products.Queries.GetList;
using Application.Features.Products.Queries.GetWithEvents;
using AutoMapper;
using Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public ProductController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<List<Product>> GetProductListAsync()
        {
            var value = await mediator.Send(new GetProductListQuery());
            return value;
        }

        [HttpGet]
        [Route("product-by-id")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var value = await mediator.Send(new GetProductByIdQuery() { Id = id });
            return (value != null ? Ok(value) : NotFound());
        }


        [HttpPost]
        public async Task<ActionResult<Product>> AddProductAsync([FromBody] CreateProductCommand product)
        {
            var value = await mediator.Send(product);
            return (value != null ? Ok(value) : BadRequest());
        }


        [HttpPut]
        [Route("product-by-id")]
        public async Task<ActionResult<Product>> UpdateProductAsync([FromBody] ProductDto productDto, int id)
        {
            UpdateProductCommand product = _mapper.Map<UpdateProductCommand>(productDto);
            product.Id = id;
            var value = await mediator.Send(product);
            return (value != null ? Ok(value) : NotFound(product));
        }


        [HttpDelete]
        [Route("product-by-id")]
        public async Task<ActionResult> DeleteProductAsync(int id)
        {
            var value = await mediator.Send(new DeleteProductCommand() { Id = id });
            return (value != 0 ? Ok() : NotFound());
        }

    }
}