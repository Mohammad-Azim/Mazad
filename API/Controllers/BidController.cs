
using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Commands.Delete;
using Application.Features.Bids.Commands.Update;
using Application.Features.Bids.Dtos;
using Application.Features.Bids.Queries.GetList;
using Application.Features.Bids.Queries.GetWithEvents;
using AutoMapper;
using Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper _mapper;

        public BidController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this._mapper = mapper;
        }


        [HttpGet]
        public async Task<List<Bid>> GetBidListAsync()
        {
            var value = await mediator.Send(new GetBidListQuery());
            return value;
        }

        [HttpGet("bidId")]
        public async Task<ActionResult<User>> GetBidByIdAsync(int bidId)
        {
            var value = await mediator.Send(new GetBidByIdQuery() { Id = bidId });
            return (value != null ? Ok(value) : NotFound());
        }



        [HttpPost]
        public async Task<ActionResult<Bid>> AddProductAsync([FromBody] CreateBidCommand bid)
        {
            var value = await mediator.Send(bid);
            return (value != null ? Ok(value) : BadRequest());
        }

        [HttpPut("bidId")]
        public async Task<ActionResult<Product>> UpdateProductAsync([FromBody] BidDto bidDto, int bidId)
        {
            UpdateBidCommand bid = _mapper.Map<UpdateBidCommand>(bidDto);
            bid.Id = bidId;
            var value = await mediator.Send(bid);
            return (value != null ? Ok(value) : NotFound(bid));
        }

        [HttpDelete("bidId")]
        public async Task<ActionResult> DeleteBidAsync(int bidId)
        {
            var value = await mediator.Send(new DeleteBidCommand() { Id = bidId });
            return (value != 0 ? Ok() : NotFound());
        }



    }
}