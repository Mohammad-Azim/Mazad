using API.myHub;
using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Commands.Delete;
using Application.Features.Bids.Commands.Update;
using Application.Features.Bids.Queries.BidListByProduct;
using Application.Features.Bids.Queries.GetList;
using Application.Features.Bids.Queries.GetWithEvents;
using AutoMapper;
using Domain.EntityModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IHubContext<BidHub> _hubContext;


        public BidController(IMediator mediator, IMapper mapper, IHubContext<BidHub> hubContext)
        {
            _mediator = mediator;
            _mapper = mapper;
            _hubContext = hubContext;
        }

        [HttpGet]
        public async Task<ActionResult<GetListBidQueryResponse>> GetBidListAsync()
        {
            var value = await _mediator.Send(new GetBidListQuery());
            return Ok(value);
        }

        [HttpGet]
        [Route("bid-by-id")]
        public async Task<ActionResult<GetBidByIdQueryResponse>> GetBidByIdAsync(int id)
        {
            var value = await _mediator.Send(new GetBidByIdQuery() { Id = id });
            return Ok(value);
        }

        [HttpGet]
        [Route("bid-by-product")]
        public async Task<ActionResult<GetBidByIdQueryResponse>> GetBidByProductIdAsync([FromQuery] int productId)
        {
            var value = await _mediator.Send(new GetBidListByProductQuery() { Id = productId });
            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBidCommandResponse>> AddBidAsync([FromBody] CreateBidCommand bid)
        {
            var value = await _mediator.Send(bid);
            await _hubContext.Clients.All.SendAsync("BidAdded", bid);
            return Ok(value);
        }

        [HttpPut]
        [Route("bid-by-id")]
        public async Task<ActionResult<UpdateBidCommandResponse>> UpdateProductAsync([FromBody] CreateBidCommand bidDto, int id)
        {
            UpdateBidCommand bid = _mapper.Map<UpdateBidCommand>(bidDto);
            bid.Id = id;
            var value = await _mediator.Send(bid);
            return Ok(value);
        }

        [HttpDelete]
        [Route("bid-by-id")]
        public async Task<ActionResult<DeleteBidCommandResponse>> DeleteBidAsync(int id)
        {
            var value = await _mediator.Send(new DeleteBidCommand() { Id = id });
            return Ok(value);
        }
    }
}