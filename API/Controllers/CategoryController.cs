using Application.Features.Categories.Commands.Delete;
using Application.Features.Categories.Commands.Update;
using Application.Features.Categories.Dtos;
using Application.Features.Categories.Queries.GetList;
using Application.Features.Categories.Queries.GetWithEvents;
using Application.Features.Products.Queries.GetWithEvents;
using AutoMapper;
using Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public CategoryController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<GetListCategoryQueryResponse> GetCategoryListAsync()
        {
            return await mediator.Send(new GetCategoryListQuery());
        }

        [HttpGet]
        [Route("bid-by-id")]
        public async Task<ActionResult<GetCategoryByIdQueryResponse>> GetCategoryByIdAsync(int id)
        {
            var value = await mediator.Send(new GetCategoryByIdQuery() { Id = id });
            return value != null ? Ok(value) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Category>> AddCategoryAsync([FromBody] CategoryDto category)
        {
            var value = await mediator.Send(category);
            return value != null ? Ok(value) : BadRequest();
        }

        [HttpPut]
        [Route("bid-by-id")]
        public async Task<ActionResult<Category>> UpdateCategoryAsync([FromBody] CategoryDto categoryDto, int id)
        {
            UpdateCategoryCommand bid = _mapper.Map<UpdateCategoryCommand>(categoryDto);
            bid.Id = id;
            var value = await mediator.Send(bid);
            return value != null ? Ok(value) : NotFound(bid);
        }

        [HttpDelete]
        [Route("bid-by-id")]
        public async Task<ActionResult<DeleteCategoryCommandResponse>> DeleteBidAsync(int id)
        {
            var value = await mediator.Send(new DeleteCategoryCommand() { Id = id });
            return Ok(value);
        }


    }
}