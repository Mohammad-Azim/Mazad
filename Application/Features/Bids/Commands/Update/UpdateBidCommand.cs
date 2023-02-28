using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Dtos;
using Application.Helper.Profiles;
using MediatR;
using Application.Services.BidService;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;

namespace Application.Features.Bids.Commands.Update
{
    public record UpdateBidCommand : BidDto, IRequest<UpdateBidCommandResponse>, IMapFrom<CreateBidCommand>
    {
        public int Id { get; set; }
    }

    public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand, UpdateBidCommandResponse>
    {
        private readonly IBidService _bidService;
        private readonly IValidator<CreateBidCommand> _validator;
        private readonly IMapper _mapper;

        public UpdateBidCommandHandler(IBidService bidService, IMapper mapper, IValidator<CreateBidCommand> validator)
        {
            _bidService = bidService;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task<UpdateBidCommandResponse> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
        {
            var updateBidCommandResponse = new UpdateBidCommandResponse();
            var bidById = await _bidService.GetById(command.Id);

            if (bidById == null)
            {
                return (UpdateBidCommandResponse)updateBidCommandResponse.NotFoundResponse("Bid Not Found");
            }

            CreateBidCommand bid = _mapper.Map<CreateBidCommand>(command);
            var validationResult = await _validator.ValidateAsync(bid, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                updateBidCommandResponse.ErrorsResponse(validationResult.Errors);
            }

            if (updateBidCommandResponse.Success)
            {
                Bid bidToAdd = _mapper.Map<Bid>(command);
                var data = await _bidService.Update(bidToAdd);
                updateBidCommandResponse.SuccessResponse(data);
            }
            return updateBidCommandResponse;
        }
    }
}