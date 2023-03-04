using API.myHub;
using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Commands.Delete;
using Application.Features.Bids.Commands.Update;
using Application.Features.Bids.Queries.BidListByProduct;
using Application.Features.Bids.Queries.GetWithEvents;
using AutoMapper;
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
        [Route("bid-by-id")]
        public async Task<ActionResult<GetBidByIdQueryResponse>> GetBidByIdAsync(int id, CancellationToken cancellationToken)
        {
            var value = await _mediator.Send(new GetBidByIdQuery() { Id = id }, cancellationToken);
            return Ok(value);
        }

        [HttpGet]
        [Route("bid-by-product")]
        public async Task<ActionResult<GetBidByIdQueryResponse>> GetBidByProductIdAsync([FromQuery] int productId, CancellationToken cancellationToken)
        {
            var value = await _mediator.Send(new GetBidListByProductQuery() { Id = productId }, cancellationToken);
            return Ok(value);
        }

        [HttpPost]
        public async Task<ActionResult<CreateBidCommandResponse>> AddBidAsync([FromBody] CreateBidCommand bid, CancellationToken cancellationToken)
        {
            var value = await _mediator.Send(bid, cancellationToken);
            await _hubContext.Clients.All.SendAsync("BidAdded", bid, cancellationToken);
            return Ok(value);
        }

        [HttpPut]
        [Route("bid-by-id")]
        public async Task<ActionResult<UpdateBidCommandResponse>> UpdateProductAsync([FromBody] CreateBidCommand bidDto, int id, CancellationToken cancellationToken)
        {
            UpdateBidCommand bid = _mapper.Map<UpdateBidCommand>(bidDto);
            bid.Id = id;
            var value = await _mediator.Send(bid, cancellationToken);
            return Ok(value);
        }

        [HttpDelete]
        [Route("bid-by-id")]
        public async Task<ActionResult<DeleteBidCommandResponse>> DeleteBidAsync(int id, CancellationToken cancellationToken)
        {
            var value = await _mediator.Send(new DeleteBidCommand() { Id = id }, cancellationToken);
            return Ok(value);
        }
    }
}