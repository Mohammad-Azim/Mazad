using Application.Features.Bids.Commands.Create;
using Application.Features.Bids.Dtos;
using Application.Helper.Profiles;
using MediatR;
using AutoMapper;
using Domain.EntityModels;
using FluentValidation;
using Application.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Bids.Commands.Update
{
    public record UpdateBidCommand : BidDto, IRequest<UpdateBidCommandResponse>, IMapFrom<CreateBidCommand>
    {
        public int Id { get; set; }
    }

    public class UpdateBidCommandHandler : IRequestHandler<UpdateBidCommand, UpdateBidCommandResponse>
    {
        private readonly ApplicationDbContext _context;
        private readonly IValidator<CreateBidCommand> _validator;
        private readonly IMapper _mapper;

        private readonly IUpdateBidCommandResponse _response;


        public UpdateBidCommandHandler(ApplicationDbContext context, IValidator<CreateBidCommand> validator, IUpdateBidCommandResponse response, IMapper mapper)
        {
            _context = context;
            _validator = validator;
            _response = response;
            _mapper = mapper;
        }

        public async Task<UpdateBidCommandResponse> Handle(UpdateBidCommand command, CancellationToken cancellationToken)
        {
            var bidById = await _context.Bids.FirstOrDefaultAsync(x => x.Id == command.Id, cancellationToken);

            if (bidById == null)
            {
                return (UpdateBidCommandResponse)_response.NotFoundResponse("Bid Not Found");
            }

            CreateBidCommand bid = _mapper.Map<CreateBidCommand>(command);
            var validationResult = await _validator.ValidateAsync(bid, cancellationToken);

            if (validationResult.Errors.Count > 0)
            {
                _response.ErrorsResponse(validationResult.Errors);
            }

            if (_response.Success)
            {
                Bid bidToAdd = _mapper.Map<Bid>(command);
                var data = _context.Bids.Update(bidToAdd);
                await _context.SaveChangesAsync(cancellationToken);
                _response.SuccessResponse(data.Entity);
            }
            return (UpdateBidCommandResponse)_response;
        }
    }
}