using Application.Features.Products.Commands.Create;
using Application.Features.Products.Commands.Delete;
using Application.Features.Products.Commands.Update;
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
        public async Task<GetListProductQueryResponse> GetProductListAsync()
        {
            return await mediator.Send(new GetProductListQuery());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProductByIdAsync(int id)
        {
            var value = await mediator.Send(new GetProductByIdQuery() { Id = id });
            return value != null ? Ok(value) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<CreateProductCommandResponse>> AddProductAsync([FromBody] CreateProductCommand product)
        {
            var response = await mediator.Send(product);
            return Ok(response);
        }

        [HttpPut]
        [Route("product-by-id")]
        public async Task<ActionResult<UpdateProductCommandResponse>> UpdateProductAsync([FromBody] CreateProductCommand productDto, int id) // #??# make update and AddProduct both DTO or create
        // #??#  command for create and another one for update then i will need to repeat the validator 
        {
            UpdateProductCommand product = _mapper.Map<UpdateProductCommand>(productDto);
            product.Id = id;
            var value = await mediator.Send(product);
            return Ok(value);
        }

        [HttpDelete]
        [Route("product-by-id")]
        public async Task<ActionResult<DeleteProductCommandResponse>> DeleteProductAsync(int id)
        {
            var value = await mediator.Send(new DeleteProductCommand() { Id = id });
            return Ok(value);
        }
    }
}