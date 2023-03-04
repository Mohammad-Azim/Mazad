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

        private readonly IMapper _mapper;

        public ModelStateDictionary ModelState { get; set; }

        public CreateBidCommandHandler(ApplicationDbContext context, IValidator<CreateBidCommand> validator, IHubContext<BidHub> hubContext, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _mapper = mapper;
            _hubContext = hubContext;

        }

        public async Task<CreateBidCommandResponse> Handle(CreateBidCommand command, CancellationToken cancellationToken)
        {
            var createBidCommandResponse = new CreateBidCommandResponse();
            var validationResult = await _validator.ValidateAsync(command, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                createBidCommandResponse.ErrorsResponse(validationResult.Errors);
            }
            if (createBidCommandResponse.Success)
            {
                Bid bid = _mapper.Map<Bid>(command);
                bid.Date = DateTime.UtcNow;

                var data = await _context.Bids.AddAsync(bid, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                createBidCommandResponse.SuccessResponse(data.Entity);
                await _hubContext.Clients.All.SendAsync("BidAdded", command, cancellationToken);
            }
            return createBidCommandResponse;
        }
    }
}