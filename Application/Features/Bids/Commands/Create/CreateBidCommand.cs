using Application.Features.Bids.Dtos;
using MediatR;

using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Application.Context;
using Microsoft.AspNetCore.SignalR;
using Application.Helper.myHub;

namespace Application.Features.Bids.Commands.Create
{
    public record CreateBidCommand : BidDto, IRequest<CreateBidCommandResponse> { }

    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, CreateBidCommandResponse>
    {
        private readonly IValidator<CreateBidCommand> _validator;

        private readonly ApplicationDbContext _context;

        private readonly IHubContext<BidHub> _hubContext;

        private readonly ICreateBidCommandResponse _response;

        private readonly IMapper _mapper;

        public ModelStateDictionary ModelState { get; set; }

        public CreateBidCommandHandler(ApplicationDbContext context, IValidator<CreateBidCommand> validator, IHubContext<BidHub> hubContext, ICreateBidCommandResponse response, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _hubContext = hubContext;
            _response = response;
        }

        public async Task<CreateBidCommandResponse> Handle(CreateBidCommand command, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                _response.ErrorsResponse(validationResult.Errors);
            }
            if (_response.Success)
            {
                Bid bid = _mapper.Map<Bid>(command);
                bid.Date = DateTime.UtcNow;

                var data = await _context.Bids.AddAsync(bid, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                _response.SuccessResponse(data.Entity);
                await _hubContext.Clients.All.SendAsync("BidAdded", command, cancellationToken);
            }
            return (CreateBidCommandResponse)_response;
        }
    }
}