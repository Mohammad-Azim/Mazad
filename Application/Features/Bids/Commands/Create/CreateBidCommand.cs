using Application.Features.Bids.Dtos;
using MediatR;
using Application.Services.BidService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Application.Features.Bids.Commands.Create
{
    public record CreateBidCommand : BidDto, IRequest<CreateBidCommandResponse> { }

    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand, CreateBidCommandResponse>
    {
        private readonly IValidator<CreateBidCommand> _validator;

        private readonly IBidService _bidService;

        private readonly IMapper _mapper;

        public ModelStateDictionary ModelState { get; set; }

        public CreateBidCommandHandler(IBidService bidService, IValidator<CreateBidCommand> validator, IMapper mapper)
        {
            _bidService = bidService;
            _validator = validator;
            _mapper = mapper;
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
                var data = await _bidService.Create(bid);
                createBidCommandResponse.SuccessResponse(data);
            }
            return createBidCommandResponse;
        }
    }
}